using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ11
{
	class Character
	{
		private readonly uint mAddress;
        public uint sAddress;
        public uint eAddress;
        public Bag Inventory { get; set; } = new Bag();
		public String Name { get; private set; }

        public Character(uint address)
		{
            sAddress = address;
			Name = SaveData.Instance().ReadText(address + 28, 10, System.Text.Encoding.ASCII); //changed to ASCII for localized names
			mAddress = address + 27 + (uint)Name.Length; // adjusted for correct offset + correct name length adjustment

            eAddress = mAddress + 84;//set to start of first chracter data 'block'
            for (uint h = 0; h < 7; h++)//loop through the 7 "blocks" of data. Editing thse entries is not advised so they won't be split out
            {
                uint count = SaveData.Instance().ReadNumber(eAddress, 4);//how many entries in this "block"?
                eAddress += 4;//move to first entry
                for (uint i = 0; i < count; i++)
                {
                    eAddress += SaveData.Instance().ReadNumber(eAddress, 4) + 4;//read length of entry for offset + the 4 bytes for the number itself 
                }//when ths is doen we are now at the first "extra" entry, used for seed and quest buffs to raw stats
            }
        }

		public uint Lv
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 8, 4); }
			set { Util.WriteNumber(mAddress + 8, 4, value, 99, 1); }
		}

		public uint Exp
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 64, 4); }
			set { Util.WriteNumber(mAddress + 64, 4, value, 9999999, 0); }
		}

		public uint HP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 12, 4); }
			set { Util.WriteNumber(mAddress + 12, 4, value, 999, 0); }
		}

		public uint MaxHP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 48, 4); }
		}

		public uint MP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 16, 4); }
			set { Util.WriteNumber(mAddress + 16, 4, value, 999, 0); }
		}

		public uint MaxMP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 52, 4); }
		}

		public uint Attack
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 20, 4); }
		}

		public uint Defense
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 24, 4); }
		}

		public uint AttackMagic
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 40, 4); }
		}

		public uint RecoveryMagic
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 44, 4); }
		}

		public uint Speed
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 28, 4); }
		}

		public uint Skillful
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 32, 4); }
		}

		public uint Charm
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 36, 4); }
        }

        public uint Skill
        {
            get { return SaveData.Instance().ReadNumber(mAddress + 68, 4); }
        }

        public uint sMaxHP
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 28, 4); }
            set { Util.WriteNumber(eAddress + 28, 4, value, 999, 1); }
        }

        public uint sMaxMP
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 32, 4); }
            set { Util.WriteNumber(eAddress + 32, 4, value, 999, 0); }
        }

        public uint sAttack
        {
            get { return SaveData.Instance().ReadNumber(eAddress, 4); }
            set { Util.WriteNumber(eAddress, 4, value, 999, 0); }
        }

        public uint sDefense
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 4, 4); }
            set { Util.WriteNumber(eAddress + 4, 4, value, 999, 0); }
        }

        public uint sAttackMagic
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 20, 4); }
            set { Util.WriteNumber(eAddress + 20, 4, value, 999, 0); }
        }

        public uint sRecoveryMagic
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 24, 4); }
            set { Util.WriteNumber(eAddress + 24, 4, value, 999, 0); }
        }

        public uint sSpeed
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 8, 4); }
            set { Util.WriteNumber(eAddress + 8, 4, value, 999, 0); }
        }

        public uint sSkillful
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 12, 4); }
            set { Util.WriteNumber(eAddress + 12, 4, value, 999, 0); }
        }

        public uint sCharm
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 16, 4); }
            set { Util.WriteNumber(eAddress + 16, 4, value, 999, 0); }
        }

        public uint sSkill
        {
            get { return SaveData.Instance().ReadNumber(eAddress + 36, 4); }
            set { Util.WriteNumber(eAddress + 36, 4, value, 999, 0); }
        }
    }
}
