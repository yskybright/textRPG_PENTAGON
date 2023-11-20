﻿using ConsoleTables;
using EnumsNamespace;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Inventory
    {
        //List<Item> inventory = new List<Item>();
        public List<WeaponItem> weaponItem = new List<WeaponItem>();
        public List<ArmorItem> armorItem = new List<ArmorItem>();
        public List<PotionItem> potionItem = new List<PotionItem>();

        public static Inventory _instance;
        //InventorySetting
        //weapon
        //이름, 레벨, 직업, 공격력, 효과, 설명, 골드, 장착유무
        //armor
        //이름, 레벨, 직업, 방어력, 체력, 효과, 설명, 골드, 장착유무
        //potion
        //이름, 힐, MP, 효과, 설명, 골드
        public void ItemSetting()
        {
            switch (Program.player1.JobType)
            {
                case JobType.JT_Warrior:
                    WeaponItem oldSword = new WeaponItem("낡은 검", 0, JobType.JT_Warrior, 5, "공격력 +5", "흔히 볼 수 있는 검입니다.", 500, false);
                    weaponItem.Add(oldSword);

                    ArmorItem ironArmor = new ArmorItem("무쇠 갑옷", 0, JobType.JT_Warrior, 5, 0, "방어력 +5", "흔히 볼 수 있는 갑옷입니다.", 500, false);
                    armorItem.Add(ironArmor);
                    break;
                case JobType.JT_Mage:
                    WeaponItem woodenStick = new WeaponItem("나무 막대기", 0, JobType.JT_Mage, 5, "공격력 +5", "산에서 주워온 막대기입니다.", 500, false);
                    weaponItem.Add(woodenStick);

                    ArmorItem shabbyClothes = new ArmorItem("허름한 옷", 0, JobType.JT_Mage, 5, 0, "방어력 +5", "허름한 옷입니다.", 500, false);
                    armorItem.Add(shabbyClothes);
                    break;
                case JobType.JT_Thief:
                    WeaponItem dagger = new WeaponItem("단검", 0, JobType.JT_Thief, 5, "공격력 +5", "흔한 단검 입니다.", 500, false);
                    weaponItem.Add(dagger);

                    ArmorItem ShabbyNinjaClothes = new ArmorItem("허름한 닌자 옷", 0, JobType.JT_Thief, 5, 0, "방어력 +5", "허름한 닌자의 옷입니다.", 500, false);
                    armorItem.Add(ShabbyNinjaClothes);
                    break;
                case JobType.JT_Archer:
                    WeaponItem woodenBow = new WeaponItem("나무 활", 0, JobType.JT_Archer, 5, "공격력 +5", "산에서 주워온 막대기로 만들었습니다.", 500, false);
                    weaponItem.Add(woodenBow);

                    ArmorItem oldHunterClothes = new ArmorItem("낡은 사냥꾼 옷", 0, JobType.JT_Archer, 5, 0, "방어력 +5", "혼히 볼 수 있는 사냥꾼의 옷입니다.", 500, false);
                    armorItem.Add(oldHunterClothes);
                    break;
            }

            //string name, int gold, string explanation, int heal
            PotionItem HpPotion = new PotionItem("물약", 20, 0, 1, "Hp +20", "물약을 먹으면 HP가 회복됩니다.", 100);
            potionItem.Add(HpPotion);

            PotionItem MpPotion = new PotionItem("물약", 0, 20, 1, "Mp +20", "물약을 먹으면 HP가 회복됩니다.", 100);
            potionItem.Add(MpPotion);
        }

        //인벤토리 메인
        public void DispayInventoryMain()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1. 무기 인벤토리");
            Console.WriteLine("2. 무기 인벤토리 정렬");
            Console.WriteLine("3. 방어구 인벤토리");
            Console.WriteLine("4. 방어구 인벤토리 정렬");
            Console.WriteLine("5. 포션 인벤토리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, 5);
            switch (input)
            {
                case 0:
                    //0. 나가기 - 메인화면
                    GameManager.Instance.DisplayGameIntro();
                    break;
                case 1:
                    //1. 무기 인벤토리
                    WeaponInventory();
                    break;
                case 2:
                    //무기 정렬
                    WeaponInventorySort();
                    break;
                case 3:
                    //2. 방어구 인벤토리
                    ArmorInventory();
                    break;
                case 4:
                    //방어구 정렬
                    ArmorInventorySort();
                    break;
                case 5:
                    //3. 기타 인벤토리(물약)
                    ETCInventory();
                    break;
            }
        }
        
        //weaponInventory 화면 출력
        public void DisplayWeaponInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/무기");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            
            for (int i = 0; i < weaponItem.Count; i++)
            {
                //table.AddRow($"{weaponItem[i].Name} ", $"{weaponItem[i].Effect}", $"{weaponItem[i].Explanation}");
                //table.AddRow(weaponItem[i].Name, weaponItem[i].Effect, weaponItem[i].Explanation);


                //if (weaponItem[i].Name.Contains("[E]"))
                if (weaponItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {weaponItem[i].Name} ", $"{weaponItem[i].Effect}", $"{weaponItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{weaponItem[i].Name} ", $"{weaponItem[i].Effect}", $"{weaponItem[i].Explanation}");
                }
            }
            table.Write();
            Console.WriteLine();
        }
        //무기 인벤토리 - 무기 장착 및 해제
        public void WeaponInventory()
        {
            DisplayWeaponInventory();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            
            for (int i = 0; i < weaponItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weaponItem[i].Name} 장착/해제");
            }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, weaponItem.Count);
            if (input == 0)
            {
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {
                if ((weaponItem[input - 1].Level <= Program.player1.Level) && (Program.player1.JobType == weaponItem[input - 1].JobType))
                {
                    //if (player._equipmentWeaponArray == null)
                    if (weaponItem[input - 1].IsEquip == false)
                    {
                        //Item에서 구현 ㄱㄱ
                        weaponItem[input - 1].IsEquip = true;
                        //_equipmentWeaponArray.Add(weaponItem[input - 1]);
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.Damage += weaponItem[input - 1].Atk;
                    }
                    else
                    {
                        //해제 IsEquip = false;
                        //장착 IsEquip = true;
                        //플레이어 += weapon.atk;
                        //플레이어 += weapon.def;
                        //플레이어 += weapon.hp;
                        //if (player._equipmentWeaponArray != null)
                        //{
                        //    weaponItem.Add(player._equipmentWeaponArray);
                        //}
                        weaponItem[input - 1].IsEquip = false;
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.Damage -= weaponItem[input - 1].Atk;
                    }
                }
            }
        }
        //ArmorInventory 화면 출력
        public void DisplayArmorInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/방어구");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            for (int i = 0; i < armorItem.Count; i++)
            {
                //if (armorItem[i].Name.Contains("[E]"))
                if (armorItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {armorItem[i].Name} ", $"{armorItem[i].Effect}", $"{armorItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{armorItem[i].Name} ", $"{armorItem[i].Effect}", $"{armorItem[i].Explanation}");
                }
            }
            table.Write();
        }
        //방어구 인벤토리 - 방어구 장착 및 해제
        public void ArmorInventory()
        {
            DisplayArmorInventory();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            for (int i = 0; i < armorItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {armorItem[i].Name} 장착/해제");
            }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, armorItem.Count);
            if (input == 0)
            {
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {
                //장착/해제 구현
                //일단 armorItem중 장착된 armorItem이 있는지 확인
                //if (armorItem[input - 1].IsEquip == false)

                if ((armorItem[input - 1].Level <= Program.player1.Level) && (Program.player1.JobType == armorItem[input - 1].JobType))
                {
                    if (armorItem[input - 1].IsEquip == false)
                    {
                        armorItem[input - 1].IsEquip = true;
                        //_equipmentArmorArray.Add(armorItem[input - 1]);

                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.Defence += armorItem[input - 1].Def;
                        Program.player1.MaxHp += armorItem[input - 1].MaxHp;
                    }
                    else
                    {

                        //if (player._equipmentWeaponArray != null)
                        //{
                        //    weaponItem.Add(player._equipmentWeaponArray);
                        //}
                        weaponItem[input - 1].IsEquip = false;
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.Defence -= weaponItem[input - 1].Def;
                        Program.player1.MaxHp -= weaponItem[input - 1].MaxHp;
                    }
                }
            }
        }

        //기타 인벤토리 - 물약
        public void ETCInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/기타 아이템");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            //포션의 개수를 표기추가하자
            for (int i = 0; i < potionItem.Count; i++)
            {
                if (potionItem.Count >= 1)
                {
                    table.AddRow($"{potionItem[i].Name} x{potionItem[i].Count} ", $"{potionItem[i].Effect}", $"{potionItem[i].Explanation}");
                }
                else
                {
                    Console.WriteLine("비어있습니다.");
                }
            }
            table.Write();

            int input = CheckValidInput(0, potionItem.Count);
            if (input == 0)
            {
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {

                Console.WriteLine($"{potionItem[input - 1].Name}을 먹겠습니까?");
                Console.WriteLine("0. 아니요");
                Console.WriteLine("1. 네");
                CheckValidInput(0, 1);
                switch (input)
                {
                    case 0:
                        //아니요 - 뒤로가기
                        ETCInventory();
                        break;
                    case 1:
                        //냠냠
                        //EatPotion();
                        break;
                }
            }
        }

        //인벤토리 정렬
        public void WeaponInventorySort()//List<Item> weaponItem
        {
            DisplayWeaponInventory();
            //int input = CheckValidInput(0, Count);
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int input = CheckValidInput(0, weaponItem.Count);
            switch (input)
            {
                case 0:
                    //나가기
                    DispayInventoryMain();
                    break;
                case 1:
                    //공격력 높은 순으로 정렬
                    List<WeaponItem> weaponItemSort = weaponItem.OrderBy(x => x.Atk).Reverse().ToList();
                    WeaponInventorySort();
                    break;
                case 2:
                    //공격력 낮은 순으로 정렬
                    List<WeaponItem> weaponItemSort2 = weaponItem.OrderBy(x => x.Atk).ToList();
                    WeaponInventorySort();
                    break;
            }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
        }

        //방어구 정렬
        public void ArmorInventorySort()
        {
            DisplayArmorInventory();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            //int input = CheckValidInput(0, Count);
            int input = CheckValidInput(0, armorItem.Count);
            switch (input)
            {
                case 0:
                    //나가기
                    DispayInventoryMain();
                    break;
                case 1:
                    //방어력 높은 순으로 정렬
                    List<ArmorItem> armorItemSort = armorItem.OrderBy(x => x.Atk).Reverse().ToList();
                    ArmorInventorySort();
                    break;
                case 2:
                    //방어력 낮은 순으로 정렬
                    List<ArmorItem> armorItemSort2 = armorItem.OrderBy(x => x.Atk).ToList();
                    ArmorInventorySort();
                    break;
            }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
        }


        //입력값 확인
        public static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

    }
}
