using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DQ11
{
	class ViewModel
	{
        public uint moneyaddr;
        public uint bankaddr;
        public uint DQaddress;
        public ObservableCollection<Character> Party { get; set; } = new ObservableCollection<Character>();
		public Bag Items { get; set; } = new Bag();//item bag
        public Bag EItems { get; set; } = new Bag();//equipment bag
        public Bag SItems { get; set; } = new Bag();//story items bag
        public Bag RItems { get; set; } = new Bag();//obtained recipe book items. not used in this version of the editor, but needed for offsets
        public Bag CItems { get; set; } = new Bag();//currency items (not gold)

        public ViewModel()
		{
            foreach (var item in SaveData.Instance().FindAddress("JackFriendGameCharacter", 0))
			{
				Party.Add(new Character(item));
                moneyaddr = Party[0].sAddress - 41;
                bankaddr = Party[0].sAddress - 33;
            }

            var DQIndex = SaveData.Instance().FindAddress("DLC_00", 0);
            if (DQIndex.Count == 0) return;
            DQaddress = DQIndex[0] - 48;//No shopping offset. Other 9 seem to be mixed around at +0x04 each from here
            
            var itemIndex = SaveData.Instance().FindAddress("DLC_07", 0);
            if (itemIndex.Count == 0) itemIndex = SaveData.Instance().FindAddress("DLC_02", 0);//accounts for PC version difference
            if (itemIndex.Count == 0) return;
			uint address = itemIndex[0] + 11;

			for(int i = 0; i < Party.Count; i++)
			{
				address = Party[i].Inventory.Create(address);
                address = Party[i].TTInventory.Create(address);//coppied inventory from Act 1 that gets restored in Act 3. Not currently used in editor
            }

			address = Items.Create(address);//Item bag
            address += 4;
            address = EItems.Create(address);//Equipment bag
            address += 4;
            address = SItems.Create(address);//Story Items
            address += 4;
            address = RItems.Create(address);//Recipe Items. only used to increment offset for now
            address += 4;
            address = CItems.Create(address);//Currency itmes. perfectionist pearls, mini medals, and casino tokens. Gold/Bank ammounts are handled elsewhere.
        }

        public uint Money
        {
            get { return SaveData.Instance().ReadNumber(moneyaddr, 4); }
            set { Util.WriteNumber(moneyaddr, 4, value, 9999999, 0); }
        }

        public uint Bank
        {
            get { return SaveData.Instance().ReadNumber(bankaddr, 4)/1000; }
            set { Util.WriteNumber(bankaddr, 4, value*1000, 9999000, 0); }
        }

        public uint DQ1 //No Shopping - Checkbox 1
        {
            get { return SaveData.Instance().ReadNumber(DQaddress, 4); }
            set { Util.WriteNumber(DQaddress, 4, value, 1, 0); }
        }

        public uint DQ2 //No Armor - Checkbox 2
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 4, 4); }
            set { Util.WriteNumber(DQaddress + 4, 4, value, 1, 0); }
        }

        public uint DQ3 //Super Shypox - Checkbox 6
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 8, 4); }
            set { Util.WriteNumber(DQaddress + 8, 4, value, 1, 0); }
        }

        public uint DQ4 //Shypox - Checkbox 5
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 12, 4); }
            set { Util.WriteNumber(DQaddress + 12, 4, value, 1, 0); }
        }

        public uint DQ5 //Party Wiped Out if Protagonist Perishes - Checkbox 8
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 16, 4); }
            set { Util.WriteNumber(DQaddress + 16, 4, value, 1, 0); }
        }

        public uint DQ6 //All Enemies are Super Strong - Checkbox 4
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 20, 4); }
            set { Util.WriteNumber(DQaddress + 20, 4, value, 1, 0); }
        }

        public uint DQ7 //Reduced Experience from Easy Fights - Checkbox 3
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 24, 4); }
            set { Util.WriteNumber(DQaddress + 24, 4, value, 1, 0); }
        }

        public uint DQ8 //Townsfolk Talk Tripe - Checkbox 7
        {
            get { return SaveData.Instance().ReadNumber(DQaddress + 28, 4); }
            set { Util.WriteNumber(DQaddress + 28, 4, value, 1, 0); }
        }
    }
}
