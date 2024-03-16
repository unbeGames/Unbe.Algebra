namespace Unbe.Algebra {
  /// <summary>Extrinsic rotation order. Specifies in which order rotations around the principal axes (x, y and z) are to be applied.</summary>
  public enum RotationOrder : byte {
    /// <summary>Extrinsic rotation around the x axis, then around the y axis and finally around the z axis.</summary>
    xyz,
    /// <summary>Extrinsic rotation around the x axis, then around the z axis and finally around the y axis.</summary>
    xzy,
    /// <summary>Extrinsic rotation around the y axis, then around the x axis and finally around the z axis.</summary>
    yxz,
    /// <summary>Extrinsic rotation around the y axis, then around the z axis and finally around the x axis.</summary>
    yzx,
    /// <summary>Extrinsic rotation around the z axis, then around the x axis and finally around the y axis.</summary>
    zxy,
    /// <summary>Extrinsic rotation around the z axis, then around the y axis and finally around the x axis.</summary>
    zyx,
    /// <summary>Default rotation order.</summary>
    standard = zxy
  };
}
