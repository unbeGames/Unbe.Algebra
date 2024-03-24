using System.Linq;
using Unbe.Algebra.CodeGen.Properties;
using static Unbe.Algebra.CodeGen.Utils;

namespace Unbe.Algebra.CodeGen {
  internal class VectorGenerator : BaseTypeGenerator {
    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace(dimensionX.ToString(), string.Empty);

      if(IsBoolean(numFlags)) {
        AddIndexer("int", "uint", "internal");
        AddBoolBitOperators();
        AddEquality();
      } else {
        AddProps();
        AddIndexer(T);
        AddConstructors();
        AddAssingOperators();
        AddBaseMath();
        AddBitOperators();
        AddShuffles();
        AddEquality();
        AddStringMethods();

        AddAdditionalConstructors();
        AddMath();
      }

      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }

    private void AddProps() {
      string propsTemplate = string.Empty;
      switch (dimensionX) {
        case 4:
          propsTemplate = Resources.PropsVector4;
          break;
        case 3:
          propsTemplate = Resources.PropsVector3;
          break;
        case 2:
          propsTemplate = Resources.PropsVector2;
          break;
      }
      
      sb.Append(string.Format(Resources.PropertiesVector, typeName, T));
      sb.Append(string.Format(propsTemplate, typeName, T));
    }

    private void AddIndexer(string T, string index = "int", string modifier = "public") {
      sb.Append(string.Format(Resources.VectorIndexer, typeName, T, dimensionX, index, modifier));
    }

    private void AddConstructors() {
      string vectorNConstructorTemplate = string.Empty;
      switch (dimensionX) {
        case 4:
          vectorNConstructorTemplate = Resources.ConstructorVector4;
          break;
        case 3:
          vectorNConstructorTemplate = Resources.ConstructorVector3;
          break;
        case 2:
          vectorNConstructorTemplate = Resources.ConstructorVector2;
          break;
      }

      sb.Append(string.Format(vectorNConstructorTemplate, typeName, T, vectorPrefix, typeNameBase));

      sb.Append(string.Format(Resources.SimpleConstructorVector, typeName, T, vectorPrefix));

      if (dimensionX == 3) {
        sb.Append(string.Format(Resources.SimpleConstructorOddVector, typeName, T, vectorPrefix));
      } else {
        sb.Append(string.Format(Resources.SimpleConstructorEvenVector, typeName, T, vectorPrefix));
      }

      sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, T));
      if (T != "int") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "int"));
      }
      if (T != "uint") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "uint"));
      }
      if (T != "float") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "float"));
      }
      if (T != "double") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "double"));
      }
    }

    private void AddAssingOperators() {
      for(int i = 0; i < conversionBaseTypes.Length; i++) {
        sb.Append(SingleToVectorOperator(typeName, T, conversionBaseTypes[i]));
      }
      sb.Append(SingleToVectorOperator(typeName, T, vectorType));

      var conversionVectorTypes = Utils.conversionVectorTypes[dimensionX];
      for (int i = 0; i < conversionVectorTypes.Length; i++) {
        var nextType = conversionVectorTypes[i];
        if (nextType != typeName) {
          sb.Append(VectorToVectorOperator(typeName, nextType));
        }
      }
    }

    private void AddBoolBitOperators() {
      sb.Append(string.Format(Resources.BoolBitOperators, typeName, T, vectorPrefix));
    }

    private void AddBitOperators() {
      sb.Append(string.Format(Resources.EqualityOperatorsVector, typeName, T, vectorPrefix, dimensionX));
      if (SupportsBitOps(numFlags)) {
        var rightOp = IsSigned(numFlags) ? "ShiftRightArithmetic" : "ShiftRightLogical";
        sb.Append(string.Format(Resources.ShiftOperatorsVector, typeName, vectorPrefix, rightOp));
        sb.Append(string.Format(Resources.BitOperatorsVector, typeName, T, vectorPrefix));
      }
    }

    private void AddShuffles() {
      if (dimensionX == 2) return;

      tmp.Clear();

      var shuffleTemplate = Resources.Shuffle;
      var shuffleReadonlyTemplate = Resources.ShuffleReadonly;
      var shuffleNames = dimensionX == 3 ? shuffle3Names : shuffle4Names;

      for (int i = 0; i < shuffleNames.Length; i++) {
        var shuffle = shuffleNames[i];
        var uniqueMembers = shuffle.Distinct().Count() == dimensionX;
        if (uniqueMembers) {
          var writeShuffle = shuffleInverse[shuffle];
          tmp.Append(string.Format(shuffleTemplate, typeName, shuffle, vectorPrefix, dimensionX, writeShuffle, T));
        } else {
          tmp.Append(string.Format(shuffleReadonlyTemplate, typeName, shuffle, vectorPrefix, dimensionX, shuffle, typeName));
        }
      }

      // generate 4 -> 3 shuffles
      if(dimensionX == 4) {
        var shuffle4To3Template = Resources.ShuffleToVector3;
        var reducedTypeName = $"{typeNameBase}{dimensionX - 1}";
        shuffleNames = shuffle4To3Names;

        for (int i = 0; i < shuffleNames.Length; i++) {
          var shuffle3 = shuffleNames[i];
          var uniqueMembers = shuffle3.Distinct().Count() == dimensionX - 1;

          if (uniqueMembers) {
            var missingLetter = shuffle4To3Missing[shuffle3];
            var index = shufflePositions[missingLetter];
            var shuffle4 = $"{shuffle3}{missingLetter}";
            var writeShuffle = shuffleInverse[shuffle4];
            tmp.Append(string.Format(shuffle4To3Template, reducedTypeName, shuffle3, vectorPrefix, dimensionX, shuffle4, writeShuffle, index, dimensionX - 1, T));
          } else {
            var shuffle4 = $"{shuffle3}w";
            tmp.Append(string.Format(shuffleReadonlyTemplate, reducedTypeName, shuffle3, vectorPrefix, dimensionX, shuffle4));
          }
        }

        // Vector 4 to 2
        ShuffleToVector2(shuffleReadonlyTemplate, shuffle4To2Names, 2);
      } else if(dimensionX == 3) {
        ShuffleToVector4(shuffleReadonlyTemplate, shuffle3To4Names, 1);
        // Vector 3 to 2
        ShuffleToVector2(shuffleReadonlyTemplate, shuffle3To2Names, 1);
      }


      sb.AppendLine();
      sb.Append(string.Format(Resources.ShuffleBase, tmp.ToString()));
    }

    private void ShuffleToVector4(string shuffleReadonlyTemplate, string[] shuffleNames, int enlarge) {
      var enlargedTypeName = $"{typeNameBase}{dimensionX + enlarge}";
      for (int i = 0; i < shuffleNames.Length; i++) {
        var shuffle = shuffleNames[i];
        tmp.Append(string.Format(shuffleReadonlyTemplate, enlargedTypeName, shuffle, vectorPrefix, dimensionX + enlarge, shuffle));
      }
    }

    private void ShuffleToVector2(string shuffleReadonlyTemplate, string[] shuffleNames, int reduce) {
      var shuffleToVector2 = Resources.ShuffleToVector2;
      var reducedTypeName = $"{typeNameBase}{dimensionX - reduce}";
      for (int i = 0; i < shuffleNames.Length; i++) {
        var shuffle2 = shuffleNames[i];
        var uniqueMembers = shuffle2.Distinct().Count() == dimensionX - reduce;
        var shuffle4 = dimensionX == 3 ? $"{shuffle2}z" : $"{shuffle2}zw";
        if (uniqueMembers) {
          var indexes = shuffle2Indexes[shuffle2];
          tmp.Append(string.Format(shuffleToVector2, reducedTypeName, shuffle2, vectorPrefix, dimensionX, shuffle4, indexes[0], indexes[1]));
        } else {
          tmp.Append(string.Format(shuffleReadonlyTemplate, reducedTypeName, shuffle2, vectorPrefix, dimensionX, shuffle4));
        }
      }
    }

    private void AddBaseMath() {
      sb.Append(string.Format(Resources.BaseMathOperatorsVector, typeName, T, vectorPrefix));
    }

    private void AddMath() {
      sbMath.Append(string.Format(Resources.CoreMath, typeName, vectorPrefix, T));
      if (IsIntegralNumeric(numFlags)) {
        sbMath.Append(string.Format(Resources.IntegralNumericsMath, typeName, vectorPrefix));
        if (IsBit16(numFlags)) {
          sbMath.Append(string.Format(Resources.IntegralNumeric16, typeName, vectorPrefix));
        }
        if (IsBit32(numFlags)) {
          sbMath.Append(string.Format(Resources.IntegralNumeric32, typeName, vectorPrefix));
        }
        if (IsBit64(numFlags)) {
          sbMath.Append(string.Format(Resources.IntegralNumeric64, typeName, vectorPrefix));
        }
      }
      if (IsSigned(numFlags)) {
        sbMath.Append(string.Format(Resources.SignMath, typeName, vectorPrefix));
      }
      if (IsFloatingPoint(numFlags)) {
        sbMath.Append(string.Format(Resources.FloatingPointMath, typeName, vectorPrefix));
        sbMath.Append(string.Format(Resources.Trigonometry, typeName, vectorPrefix));
      }
      sbMath.Append(string.Format(Resources.OperationsVector, typeName, vectorPrefix, T));
      if (IsFloatingPoint(numFlags)) {
        sbMath.Append(string.Format(Resources.OperationsFloatingPointVector, typeName, vectorPrefix, T));
      }
      sbMath.Append(string.Format(Resources.BooleanMath, typeName, vectorPrefix, T, dimensionX));
    }

    private void AddAdditionalConstructors() {
      string template = null;
      switch (dimensionX) {
        case 2:
          template = Resources.FactoryVector2;
          break;
        case 3:
          template = Resources.FactoryVector3;
          break;
        case 4:
          template = Resources.FactoryVector4;
          break;
      }
      sbMath.Append(string.Format(template, typeName, T, typeNameBase, dimensionX));
      for(int i = 0; i < conversionBaseTypes.Length; i++) {
        sbMath.Append(string.Format(Resources.FactoryVector, typeName, T, conversionBaseTypes[i], dimensionX));
      }

      var conversionVectorTypes = Utils.conversionVectorTypes[dimensionX];
      for (int i = 0; i < conversionVectorTypes.Length; i++) {
        var nextType = conversionVectorTypes[i];
        if (nextType != typeName) {
          sbMath.Append(string.Format(Resources.FactoryVector, typeName, T, nextType, dimensionX));
        }
      }
    }

    private string Test() {
      return
$@"

";
    }

    private void AddEquality() {
      sb.Append(string.Format(Resources.EqualsMethodsVector, typeName));
    }

    private void AddStringMethods() {
      if (dimensionX == 4) {
        sb.Append(string.Format(Resources.StringMethodsVector4, typeName));
      } else if (dimensionX == 3) {
        sb.Append(string.Format(Resources.StringMethodsVector3, typeName));
      } else if (dimensionX == 2) {
        sb.Append(string.Format(Resources.StringMethodsVector2, typeName));
      }
    }

    private string SingleValueConstructor(string typeName, string T, string vectorPrefix, string targetType) {
      string result;
      if (dimensionX == 3) {
        result = string.Format(Resources.SingleValueConstructorOdd, typeName, T, vectorPrefix, targetType);
      } else {
        result = string.Format(Resources.SingleValueConstructorEven, typeName, T, vectorPrefix, targetType);
      }
      return result;
    }

    private string SingleToVectorOperator(string typeName, string T, string targetType) {
      return string.Format(Resources.SingleToVectorOperator, ConvertToSingleArgs(typeName, T, targetType));
    }

    private string VectorToVectorOperator(string typeName, string targetType) {
      return string.Format(Resources.VectorToVectorOperator, ConvertToVectorArgs(typeName, targetType));
    }

    private string[] ConvertToSingleArgs(string typeName, string T, string targetType) {
      var operatorKind = ConvertOperator(targetType, T);
      var operatorSuffixed = AddSuffixLyCap(operatorKind);
      var hint = T == targetType ? string.Empty : $"converting it to {T} and ";
      return new string[] { typeName, hint, targetType, operatorKind, operatorSuffixed };
    }

    private string[] ConvertToVectorArgs(string typeName, string targetType) {
      var operatorKind = ConvertOperator(targetType, typeName);
      var operatorSuffixed = AddSuffixLyCap(operatorKind);
      return new string[] { typeName, targetType, operatorKind, operatorSuffixed };
    }
  }
}
