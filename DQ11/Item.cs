﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DQ11
{
	class Item : INotifyPropertyChanged
	{
		private readonly uint mAddress;

		public event PropertyChangedEventHandler PropertyChanged;

		public Item(uint address)
		{
			mAddress = address;
		}

		public void Delete()
		{
			uint size = SaveData.Instance().ReadNumber(mAddress, 4) + 13;
			SaveData.Instance().DeleteBlock(mAddress, size);
		}

		public String Name
		{
			get
			{
				uint size = SaveData.Instance().ReadNumber(mAddress, 4);
				return SaveData.Instance().ReadText(mAddress + 4, size, System.Text.Encoding.ASCII);
			}

			set
			{
				uint size = SaveData.Instance().ReadNumber(mAddress, 4);
				if (value.Length + 1 > size) SaveData.Instance().AppendBlock(mAddress + 4, (uint)value.Length - size);
				else if (value.Length + 1 < size) SaveData.Instance().DeleteBlock(mAddress + 4, size - (uint)value.Length);
				SaveData.Instance().WriteNumber(mAddress, 4, (uint)value.Length + 1);
				SaveData.Instance().WriteText(mAddress + 4, (uint)value.Length, value, System.Text.Encoding.ASCII);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
			}
		}

		public uint Count
		{
			get
			{
				uint address = CalcAddress();
				return SaveData.Instance().ReadNumber(address, 4);
			}
			set
			{
				uint address = CalcAddress();
				Util.WriteNumber(address, 4, value, SaveData.Instance().ReadNumber(address+4, 4), 0);//now dynamicly pulls max stack value to allow for certain edits
			}
		}

		public uint Max
		{
			get
			{
				uint address = CalcAddress() + 4;
				return SaveData.Instance().ReadNumber(address, 4);
			}
		}

		public uint Equipped
		{
			get
			{
				uint address = CalcAddress() + 8;
				return SaveData.Instance().ReadNumber(address, 1);
			}
		}

		private uint CalcAddress()
		{
			// ItemID Length + ItemID Name.
			return mAddress + SaveData.Instance().ReadNumber(mAddress, 4) + 4;
		}
	}
}
