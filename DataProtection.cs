using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VPN
{
	// Token: 0x0200000E RID: 14
	internal class DataProtection
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00006334 File Offset: 0x00004534
		public void Test()
		{
			string strToBeEncrypted = "test";
			byte[] cipherText = this.MyEncrypt(strToBeEncrypted);
			string text = this.MyDecrypt(cipherText);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006358 File Offset: 0x00004558
		public byte[] MyEncrypt(string strToBeEncrypted)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(strToBeEncrypted);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.sSalt);
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < 8; i++)
			{
				stringBuilder2.Append("," + stringBuilder.Length.ToString());
			}
			byte[] bytes2 = Encoding.ASCII.GetBytes(stringBuilder2.ToString());
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(stringBuilder.ToString(), bytes2, 10000);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(32);
			rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(16);
			byte[] result = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(bytes, 0, bytes.Length);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006498 File Offset: 0x00004698
		public string MyDecrypt(byte[] cipherText2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.sSalt);
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < 8; i++)
			{
				stringBuilder2.Append("," + stringBuilder.Length.ToString());
			}
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder2.ToString());
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(stringBuilder.ToString(), bytes, 10000);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(32);
			rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(16);
			byte[] bytes2 = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(cipherText2, 0, cipherText2.Length);
				}
				bytes2 = memoryStream.ToArray();
			}
			return Encoding.Unicode.GetString(bytes2);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000065D4 File Offset: 0x000047D4
		public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(initVector);
			byte[] bytes2 = Encoding.ASCII.GetBytes(saltValue);
			byte[] bytes3 = Encoding.UTF8.GetBytes(plainText);
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, bytes2, hashAlgorithm, passwordIterations);
			byte[] bytes4 = passwordDeriveBytes.GetBytes(keySize / 8);
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = CipherMode.CBC
			}.CreateEncryptor(bytes4, bytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes3, 0, bytes3.Length);
			cryptoStream.FlushFinalBlock();
			byte[] inArray = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000668C File Offset: 0x0000488C
		public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(initVector);
			byte[] bytes2 = Encoding.ASCII.GetBytes(saltValue);
			byte[] array = Convert.FromBase64String(cipherText);
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, bytes2, hashAlgorithm, passwordIterations);
			byte[] bytes3 = passwordDeriveBytes.GetBytes(keySize / 8);
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = CipherMode.CBC
			}.CreateDecryptor(bytes3, bytes);
			MemoryStream memoryStream = new MemoryStream(array);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
			byte[] array2 = new byte[array.Length];
			int count = cryptoStream.Read(array2, 0, array2.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(array2, 0, count);
		}

		// Token: 0x04000056 RID: 86
		private string sSalt = "watching@TVabroad.com";
	}
}
