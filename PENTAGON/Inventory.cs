﻿using ConsoleTables;
using EnumsNamespace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Inventory
    {
        List<Item> inventory = new List<Item>();
        List<WeaponItem> weaponItem = new List<WeaponItem>();
        List<ArmorItem> armorItem = new List<ArmorItem>();
        List<PotionItem> potionItem = new List<PotionItem>();
        Player player;
        //InventorySetting
        public void ItemSetting()
        {
            ArmorItem ironArmor = new ArmorItem("무쇠 갑옷", 0, 0, 5, 10, 100, "흔히 볼 수 있는 갑옷입니다.", JobType.JT_Warrior, false);
            armorItem.Add(ironArmor);

            WeaponItem oldSword = new WeaponItem("낡은 검", 0, 5, 0, 10, 100, "흔히 볼 수 있는 검입니다.", JobType.JT_Warrior, false);
            weaponItem.Add(oldSword);

            PotionItem potion = new PotionItem("물약", 50, "물약을 먹으면 HP가 회복됩니다.", 100);
            potionItem.Add(potion);
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
                    Program.DisplayGameIntro();
                    break;
                case 1:
                    //1. 무기 인벤토리
                    WeaponInventory();
                    break;
                case 2:
                    //무기 정렬
                    break;
                case 3:
                    //2. 방어구 인벤토리
                    ArmorInventory();
                    break;
                case 4:
                    //방어구 정렬
                    WeaponInventory();
                    break;
                case 5:
                    //3. 기타 인벤토리(물약)
                    ETCInventory();
                    break;
            }
        }
       
        //무기 인벤토리 - 무기 장착 및 해제
        public void WeaponInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/무기");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            for (int i = 0; i < weaponItem.Count; i++)
            {
                //if (weaponItem[i].Name.Contains("[E]"))
                if (weaponItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {weaponItem[i].Name} ", $"공격력:{weaponItem[i].Atk} 방어력:{weaponItem[i].Atk} 체력:{weaponItem[i].Hp}", $"{weaponItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{weaponItem[i].Name} ", $"공격력:{weaponItem[i].Atk} 방어력:{weaponItem[i].Atk} 체력:{weaponItem[i].Hp}", $"{weaponItem[i].Explanation}");
                }
            }
            table.Write();

            int input = CheckValidInput(0, weaponItem.Count);
            if (input == 0)
            {
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {
                //장착/해제 구현
                //일단 weaponItem중 장착된 weaponItem이 있는지 확인
                //장착확인, 레벨확인, 직업확인
                if (weaponItem[input - 1].Level <= player.Level && player.JobType == weaponItem[input - 1].JobType)
                {
                    //if (player._equipmentWeaponArray == null)
                    if (weaponItem[input - 1].IsEquip == false)
                    {
                        //Item에서 구현 ㄱㄱ
                        weaponItem[input - 1].IsEquip = true;
                        _equipmentWeaponArray.Add(weaponItem[input - 1]);
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        player.Damage += weaponItem[input - 1].Atk;
                        player.Defence += weaponItem[input - 1].Def;
                        player.MaxHp += weaponItem[input - 1].Hp;
                        //player.MaxMp += weaponItem[input - 1].Mp;
                    }
                    else
                    {
                        //해제 IsEquip = false;
                        //장착 IsEquip = true;
                        //플레이어 += weapon.atk;
                        //플레이어 += weapon.def;
                        //플레이어 += weapon.hp;
                        if (player._equipmentWeaponArray != null)
                        {
                            weaponItem.Add(player._equipmentWeaponArray);
                        }
                        weaponItem[input - 1].IsEquip = false;
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        player.Damage -= weaponItem[input - 1].Atk;
                        player.Defence -= weaponItem[input - 1].Def;
                        player.MaxHp -= weaponItem[input - 1].Hp;
                        //player.MaxMp += weaponItem[input - 1].Mp;
                    }
                }
            }
        }

        //방어구 인벤토리 - 방어구 장착 및 해제
        public void ArmorInventory()
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
                    table.AddRow($"[E] {armorItem[i].Name} ", $"공격력:{armorItem[i].Atk} 방어력:{armorItem[i].Atk} 체력:{ armorItem[i].Hp}", $"{armorItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{armorItem[i].Name} ", $"공격력:{armorItem[i].Atk} 방어력:{armorItem[i].Atk} 체력:{armorItem[i].Hp}", $"{armorItem[i].Explanation}");
                }
            }
            table.Write();

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
                
                if (armorItem[input - 1].Level <= player.Level && player.JobType == armorItem[input - 1].JobType)
                {
                    if (armorItem[input - 1].IsEquip == false)
                    {
                        //Item에서 구현 ㄱㄱ
                        armorItem[input - 1].IsEquip = true;
                        //_equipmentArmorArray.Add(armorItem[input - 1]);
                        _equipmentWeaponArray.Add(armorItem[input - 1]);
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        player.Damage += armorItem[input - 1].Atk;
                        player.Defence += armorItem[input - 1].Def;
                        player.MaxHp += armorItem[input - 1].Hp;
                        //player.MaxMp += weaponItem[input - 1].Mp;
                    }
                    else
                    {
                        //해제 IsEquip = false;
                        //장착 IsEquip = true;
                        //플레이어 += weapon.atk;
                        //플레이어 += weapon.def;
                        //플레이어 += weapon.hp;
                        if (player._equipmentWeaponArray != null)
                        {
                            weaponItem.Add(player._equipmentWeaponArray);
                        }
                        weaponItem[input - 1].IsEquip = false;
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        player.Damage -= weaponItem[input - 1].Atk;
                        player.Defence -= weaponItem[input - 1].Def;
                        player.MaxHp -= weaponItem[input - 1].Hp;
                        //player.MaxMp += weaponItem[input - 1].Mp;
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
                table.AddRow($"{potionItem[i].Name} ", $"Hp +{potionItem[i].Heal}", $"{potionItem[i].Explanation}");
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
                //EatPotion();
            }
        }

        //인벤토리 정렬
        public void WeaponInventorySort()//List<Item> weaponItem
        {
            //int input = CheckValidInput(0, Count);
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    //나가기
                    DispayInventoryMain();
                    break;
                case 1:
                    //공격력 높은 순으로 정렬
                    //List<Item> weaponItem1 = weaponItem.OrderBy(x => x.Atk).Reverse().ToList();
                    WeaponInventorySort();
                    break;
                case 2:
                    //공격력 낮은 순으로 정렬
                    //List<Item> weaponItem2 = weaponItem.OrderBy(x => x.Atk).ToList();
                    WeaponInventorySort();
                    break;
            }
        }

        //방어구 정렬
        public void ArmorInventorySort()
        {
            //int input = CheckValidInput(0, Count);
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    //나가기
                    DispayInventoryMain();
                    break;
                case 1:
                    //방어력 높은 순으로 정렬
                    //List<Item> weaponItem1 = weaponItem.OrderBy(x => x.Atk).Reverse().ToList();
                    ArmorInventorySort();
                    break;
                case 2:
                    //방어력 낮은 순으로 정렬
                    //List<Item> weaponItem1 = weaponItem.OrderBy(x => x.Atk).ToList();
                    ArmorInventorySort();
                    break;
            }
        }
        //int atk, int def, int hp, int mp
        //public static void WeaponEquip(Player player, List<WeaponItem> weaponItem, int input)
        //{
        //    //장착 IsEquip = true;
        //    //플레이어 += weapon.atk;
        //    //플레이어 += weapon.def;
        //    //플레이어 += weapon.hp;
        //    weaponItem[input - 1].IsEquip = true;
        //    //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
        //    player.Damage += weaponItem[input - 1].Atk;
        //    player.Defence += weaponItem[input - 1].Def;
        //    player.MaxHp += weaponItem[input - 1].Hp;
        //    //player.MaxMp += weaponItem[input - 1].Mp;
        //}

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
