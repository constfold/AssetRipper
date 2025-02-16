using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AssetRipper.Core.Math
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Vector3f : IAsset, IEquatable<Vector3f>
	{
		public float X;
		public float Y;
		public float Z;

		public Vector3f(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static implicit operator Vector3f(Vector2f v2) => new Vector3f(v2.X, v2.Y, 0f);
		public static implicit operator Vector3f(Vector2i v2) => new Vector3f(v2.X, v2.Y, 0f);
		public static implicit operator Vector3f(Vector3i v3) => new Vector3f(v3.X, v3.Y, v3.Z);
		public static explicit operator Vector3f(Vector4f v4) => new Vector3f(v4.X, v4.Y, v4.Z);

		public static implicit operator System.Numerics.Vector3(Vector3f v3) => new System.Numerics.Vector3(v3.X, v3.Y, v3.Z);
		public static implicit operator Vector3f(System.Numerics.Vector3 v3) => new Vector3f(v3.X, v3.Y, v3.Z);

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return X;
					case 1: return Y;
					case 2: return Z;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Invalid Vector3 index!");
				}
			}

			set
			{
				switch (index)
				{
					case 0: X = value; break;
					case 1: Y = value; break;
					case 2: Z = value; break;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Invalid Vector3 index!");
				}
			}
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ (Y.GetHashCode() << 2) ^ (Z.GetHashCode() >> 2);
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3f))
				return false;
			return Equals((Vector3f)other);
		}

		public bool Equals(Vector3f other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
		}

		public static Vector3f operator -(Vector3f left)
		{
			return new Vector3f(-left.X, -left.Y, -left.Z);
		}

		public static Vector3f operator -(Vector3f left, Vector3f right)
		{
			return new Vector3f(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public static Vector3f operator +(Vector3f left, Vector3f right)
		{
			return new Vector3f(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static Vector3f operator *(Vector3f left, float right)
		{
			return new Vector3f(left.X * right, left.Y * right, left.Z * right);
		}

		public static Vector3f operator *(float d, Vector3f a)
		{
			return new Vector3f(a.X * d, a.Y * d, a.Z * d);
		}

		public static Vector3f operator /(Vector3f left, float right)
		{
			return new Vector3f(left.X / right, left.Y / right, left.Z / right);
		}

		public static bool operator ==(Vector3f left, Vector3f right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		public static bool operator !=(Vector3f left, Vector3f right)
		{
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		public static Vector3f Scale(Vector3f left, Vector3f right)
		{
			return new Vector3f(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		public void Read(AssetReader reader)
		{
			X = reader.ReadSingle();
			Y = reader.ReadSingle();
			Z = reader.ReadSingle();
		}

		public void Write(AssetWriter writer)
		{
			writer.Write(X);
			writer.Write(Y);
			writer.Write(Z);
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Style = MappingStyle.Flow;
			node.Add(XName, X);
			node.Add(YName, Y);
			node.Add(ZName, Z);
			return node;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0:0.00}, {1:0.00}, {2:0.00}]", X, Y, Z);
		}

		public static Vector3f One => new Vector3f(1.0f, 1.0f, 1.0f);

		public static Vector3f Zero => new Vector3f();

		public void Normalize()
		{
			var length = Length();
			if (length > kEpsilon)
			{
				var invNorm = 1.0f / length;
				X *= invNorm;
				Y *= invNorm;
				Z *= invNorm;
			}
			else
			{
				X = 0;
				Y = 0;
				Z = 0;
			}
		}

		public float Length()
		{
			return (float)System.Math.Sqrt(LengthSquared());
		}

		public float LengthSquared()
		{
			return X * X + Y * Y + Z * Z;
		}

		private const float kEpsilon = 0.00001F;
		public const string XName = "x";
		public const string YName = "y";
		public const string ZName = "z";
	}
}
