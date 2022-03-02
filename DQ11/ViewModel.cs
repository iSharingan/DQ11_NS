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
		public ObservableCollection<Character> Party { get; set; } = new ObservableCollection<Character>();
		public Bag Items { get; set; } = new Bag();//item bag
        public Bag EItems { get; set; } = new Bag();//equipment bag
        public Bag SItems { get; set; } = new Bag();//story items bag
        public Bag RItems { get; set; } = new Bag();//obtained recipe book items. not used in this version of the editor, but needed for offsets
        public Bag CItems { get; set; } = new Bag();//currency items (not gold)

        public ViewModel()
		{
			foreach(var item in SaveData.Instance().FindAddress("JackFriendGameCharacter", 0))
			{
				Party.Add(new Character(item));
                moneyaddr = Party[0].sAddress - 41;
                bankaddr = Party[0].sAddress - 33;
            }
            


            
            var itemIndex = SaveData.Instance().FindAddress("DLC_07", 0);
			if (itemIndex.Count == 0) return;
			uint address = itemIndex[0] + 11;

			for(int i = 0; i < Party.Count; i++)
			{
				address = Party[i].Inventory.Create(address);
				address += 4;
			}

			address = Items.Create(address);
            address += 4;
            address = EItems.Create(address);
            address += 4;
            address = SItems.Create(address);
            address += 4;
            address = RItems.Create(address);//only used to incriment offset for now
            address += 4;
            address = CItems.Create(address);//perfectionist pearls, mini medals, and casino tokens
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
    }
}
