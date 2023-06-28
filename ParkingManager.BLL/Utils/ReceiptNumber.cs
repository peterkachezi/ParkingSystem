using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManager.BLL.Utils
{
	public static class ReceiptNumber
	{
		public static string GenerateUniqueNumber()
		{

			string numbers = "12345678";

			string characters = numbers;
			int length = 8;
			string id = string.Empty;
			for (int i = 0; i < length; i++)
			{
				string character = string.Empty;
				do
				{
					int index = new Random().Next(0, characters.Length);
					character = characters.ToCharArray()[index].ToString();
				} while (id.IndexOf(character) != -1);
				id += character;
			}

			return id;
		}



	}
}
