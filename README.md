Modified to 'work' with most non-japanese saves by iSharingan

# NOTICE

Due to Github being retarded, consider this account derelict until forced 2FA is removed. I won't punish users of my edits on this tool, but it won't be recieving updates/fixes. If you have questions, find me on Discord as iShar#1771 (just let me know where you found me)

# Overview
Nintendo Switch Dragon Quest 11 S save data editing Tool (should also work on PC/XBox/PS4 DQ11S saves)

# Official DQ11S websites:
https://www.dq11.jp/s/pf/index.html (japanese)
https://dragonquest.square-enix-games.com/xi/en-us/ (english)

# Requirements
* Windows PC
* Means to extracted save (Checkpoint works for Switch. Steam version save works straight out of the save folder)
* Means to write back save (Checkpoint works for Switch. Steam version save can be put back in the save folder)

# Build enviroment
* Windows 7 (64bit) (should work on Windows 10. Project can be built on either)
* Visual Studio 2017 (original project was made in VS 2019)

# Editng procedure
* Extract save from system (keeping an unedited backup is strongly recommended)
   * Resulting file name should appear as:
      * dataxxx.sav(IE: data000.sav) for each save slot (autosaves are data254 and data255)
      * system999.sav (Not edited. Tracks various flags that are independant of save slot)
* Load the dataxxx.sav you wish to edit
* Make changes in the editor
* Export back to the dataxxx.sav file (again, keeping an unedited backup can fix the save in case things go wrong)
* Write the edited save back to the original location

# Special Thanks
* Decrypt/Encrypt
   * https://gbatemp.net/threads/transfering-dragon-quest-xi-s-pc-save-to-switch.549159/
* ItemID
   * http://roadbikebeginners.com/ps4-dragonquest11-cheat/
* English item names
   * https://forum.hackinformer.com/viewtopic.php?f=115&t=2253
