using System;
using System.ComponentModel;
using System.Numerics;

namespace TextGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            int inputNum;

            Console.WriteLine("===============================");
            Console.WriteLine("\t모험가 이야기");
            Console.WriteLine("\tEnter To Start");
            Console.WriteLine("===============================");
            if (Console.ReadLine().Length > 0) Console.WriteLine("게임을 시작합니다.");

            Console.WriteLine("당신은 모험을 시작하게 됩니다.");
            Console.WriteLine("초기 능력치는 다음과 같습니다.\n");
            Console.WriteLine("라이프\t {0}  정신력\t {1}  체력 {2}", player.life, player.mental, player.maxhp);
            Console.WriteLine("힘\t {0} 민첩\t {1}\n지능\t {2} 행운\t {3}",
                player.str, player.dex, player.int_, player.luk);
            Console.WriteLine("라이프 혹은 정신력이 0외 되면 사망합니다.\n");

            while (player.life > 0 && player.mental > 0)
            {
                EventOccur.eventProduce(player);
            }

            Console.WriteLine("사망");
            
        } // Main
    } // Program

    public class Player
    {
        public int maxhp = 50;
        public int nowhp = 50;
        public int life = 5;
        public int mental = 5;
        public int str = 10, dex = 10, int_ = 10, luk = 10;
        public int exp = 0;
        public int level = 1;
        public int[] levelEXP = new int[10] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
 
        public void increaseEXP(int getExp)
        {
            exp += getExp;
            if (exp >= levelEXP[level - 1])
            {
                exp -= levelEXP[level - 1];
                level++;
                str += 2;
                dex++;
                int_++;
                luk++;
                life = 5;
                maxhp += 5;
                nowhp = maxhp;
                mental = 5;

                Console.WriteLine("레벨업");
                Console.WriteLine("라이프\t {0}  정신력\t {1}  체력 {2}", life, mental, maxhp);
                Console.WriteLine("힘\t {0} 민첩\t {1}\n지능\t {2} 행운\t {3}", str, dex, int_, luk);
            }
        }// increaseEXP

        public int BehaviorSelect(int n)
        {
            int selectNum;
            while (true)
            {
                Console.Write("선택지의 숫자를 입력해 주세요.");
                int.TryParse(Console.ReadLine(), out selectNum);
                if (selectNum >= 1 && selectNum <= n) break;
                else Console.WriteLine("잘못된 입력입니다. 재 입력합니다.");
            }

            return selectNum;
        }// BehaviorSelect

        public int attack()
        {
            return str + 5;
        }

        public void Heal()
        {
            nowhp += 20;
            if(nowhp > maxhp) nowhp= maxhp;
        }
    } // Player

    public class Battle
    {
        public static void MonsterSpawn(Player player)
        {
            Random random = new Random();
            int ran = random.Next(0, 2 + 1);
            switch (ran)
            {
                case 0:
                    BattleSlime(player);
                    break;
                case 1:
                    BattleWolf(player);
                    break;
                case 2:
                    BattleOrc(player);
                    break;
                default: Console.WriteLine("에러 발생"); break;
            }
        }
        static void BattleSlime(Player player)
        {
            Random random = new Random();
            Console.WriteLine("야생의 슬라임과 대결합니다.");
            int SlimeHp = 50;
            int SlimeAttack = 7;

            while (player.nowhp > 0 && SlimeHp > 0)
            {
                int ran = random.Next(1, 100 + 1);
                if (ran + player.dex > 30)
                {
                    Console.WriteLine("플레이어의 공격 {0}의 데미지", player.attack());
                    SlimeHp -= player.attack();
                }
                else
                {
                    Console.WriteLine("슬라임의 공격 {0}의 데미지", SlimeAttack);
                    player.nowhp -= SlimeAttack;
                }
                Thread.Sleep(1000);
            }
            if (player.nowhp > 0) Win(7, player);
            else Defeat(player);
        }
        static void BattleWolf(Player player)
        {
            Random random = new Random();
            Console.WriteLine("초원의 늑대와 대결합니다.");
            int wolfHp = 75;
            int wolfAttack = 13;

            while (player.nowhp > 0 && wolfHp > 0)
            {
                int ran = random.Next(1, 100 + 1);
                if (ran + player.dex > 40)
                {
                    Console.WriteLine("플레이어의 공격 {0}의 데미지", player.attack());
                    wolfHp -= player.attack();
                }
                else
                {
                    Console.WriteLine("울프의 공격 {0}의 데미지", wolfAttack);
                    player.nowhp -= wolfAttack;
                }
                Thread.Sleep(1000);
            }
            if (player.nowhp > 0) Win(13, player);
            else Defeat(player);
        }
        
        static void BattleOrc(Player player)
        {
            Random random = new Random();
            Console.WriteLine("거대 오크와 대결합니다.");
            int orcHp = 100;
            int orcAttack = 18;

            while (player.nowhp > 0 && orcHp > 0)
            {
                int ran = random.Next(1, 100 + 1);
                if (ran + player.dex > 55)
                {
                    Console.WriteLine("플레이어의 공격 {0}의 데미지", player.attack());
                    orcHp -= player.attack();
                }
                else
                {
                    Console.WriteLine("오크의 공격 {0}의 데미지", orcAttack);
                    player.nowhp -= orcAttack;
                }
                Thread.Sleep(1000);
            }
            if (player.nowhp > 0) Win(7, player);
            else Defeat(player);
        }

        static void Win(int exp, Player player)
        {
            Console.WriteLine("승리! 경험치를 획득합니다.\n");
            player.increaseEXP(exp);
        }

        static void Defeat(Player player)
        {
            Console.WriteLine("패배! 라이프를 1 잃습니다.\n");
            player.life--;
            player.nowhp = player.maxhp;
        }
    } // Battle

    public class EventOccur
    {
        public static void eventProduce(Player player)
        {
            Random random = new Random();
            int ran = random.Next(1, 100 + 1);

            if(ran <= 10)
            {
                Console.WriteLine("치유의 샘과 마주했습니다.");
                Console.WriteLine("라이프와 정신력을 1씩 회복하며 체력이 20 회복됩니다.");
                player.life++;
                player.mental++;
                player.Heal();
                Console.WriteLine("라이프\t {0} 정신력\t {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
            }
            else if(ran > 10 && ran <= 65)
            {
                Console.WriteLine("특수 상황에 직면했습니다.");
                SpecialEvent(player);
            }
            else
            {
                Console.WriteLine("몬스터와 조우했습니다.");
                Battle.MonsterSpawn(player);
            }
        } // eventProduce

        static void SpecialEvent(Player player)
        {
            Random random = new Random();
            int ran = random.Next(1, 3 + 1);
            int playerSelectNum;

            switch (ran)
            {
                case 1: 
                    Console.WriteLine("눈 앞에서 소매치기가 도망치고 있습니다. 당신의 선택은?");
                    Console.WriteLine("1. 무시한다.\t2. 쫓아간다");
                    playerSelectNum = player.BehaviorSelect(2);
                    if(playerSelectNum == 1)
                    {
                        Console.WriteLine("당신은 죄책감에 휩싸입니다. 정신력이 2 감소합니다.");
                        player.mental -= 2;
                        Console.WriteLine("라이프\t {0} 정신력 {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
                    }
                    else
                    {
                        ran = random.Next(1, 50 + 1);
                        if (ran + player.dex >= 30)
                        {
                            Console.WriteLine("소매치기를 붙잡았습니다.");
                            Console.WriteLine("경험치 5 를 획득합니다.\n");
                            player.increaseEXP(5);
                        }
                        else
                            Console.WriteLine("소매치기를 놓쳤습니다.\n");
                    }
                    break;
                case 2:
                    Console.WriteLine("수상한 골목과 마주했습니다. 당신의 선택은?");
                    Console.WriteLine("1. 들어간다.\t2. 돌아간다.\t3. 골목에 불을 지른다.");
                    playerSelectNum = player.BehaviorSelect(3);
                    if (playerSelectNum == 1) 
                    {
                        Console.WriteLine("도적에게 습격당했습니다.");
                        player.life -= 2;
                        Console.WriteLine("라이프가 2 감소합니다.");
                        Console.WriteLine("라이프\t {0} 정신력 {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
                    }
                    else if(playerSelectNum == 2)
                    {
                        Console.WriteLine("행운이 2상승합니다.\n");
                        player.luk += 2;
                    }
                    else
                    {
                        ran = random.Next(1, 50 + 1);
                        if(ran + player.luk >= 40)
                        {
                            Console.WriteLine("숨어 있던 도적을 체포했습니다!");
                            Console.WriteLine("행운이 5 상승하며, 경험치를 획득합니다.");
                            player.increaseEXP(10);
                            player.luk += 5;
                        }
                        else
                        {
                            Console.WriteLine("당신은 화상입고 경비병에게 붙잡힙니다.");
                            Console.WriteLine("라이프가 1 감소하며, 근력과 행운이 감소합니다.");
                            player.life -= 1;
                            player.str--;
                            player.luk -= 2;
                            Console.WriteLine("라이프\t {0} 정신력 {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("경비병이 당신을 체포하려합니다. 당신의 선택은?");
                    Console.WriteLine("1. 심문당한다.\t2. 도망친다.");
                    playerSelectNum = player.BehaviorSelect(2);
                    if(playerSelectNum == 1)
                    {
                        ran = random.Next(0, 50 + 1);
                        if(ran + player.int_ >= 35)
                        {
                            Console.WriteLine("당신은 오해를 풀고 신뢰를 얻었습니다.");
                            Console.WriteLine("경비병의 치료로 라이프가 1 회복됩니다. 또한 지능이 2 상승합니다.");
                            player.life++;
                            player.int_ += 2;
                            Console.WriteLine("라이프\t {0} 정신력 {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
                        }
                        else
                        {
                            Console.WriteLine("당신은 누명을 뒤집어 쓰게 되었습니다.");
                            player.mental--;
                            player.int_ -= 2;
                            Console.WriteLine("정신력이 1 감소하며, 지능이 2 감소합니다.");
                            Console.WriteLine("라이프\t {0} 정신력 {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
                        }
                    }
                    if(playerSelectNum == 2)
                    {
                        ran = random.Next(0, 50 + 1);
                        if(ran + player.dex >= 35)
                        {
                            Console.WriteLine("당신은 도망치는데 성공했습니다.");
                            Console.WriteLine("민첩이 2 상승합니다\n");
                            player.dex += 2;
                        }
                        else
                        {
                            Console.WriteLine("당신은 체포당했습니다.");
                            Console.WriteLine("라이프와 정신력이 1 씩 감소하며, 행운이 2 감소하고, 민첩이 1 감소합니다.");
                            player.life--;
                            player.mental--;
                            player.luk -= 2;
                            player.dex--;
                            Console.WriteLine("라이프\t {0} 정신력 {1}  체력 {2}\n", player.life, player.mental, player.nowhp);
                        }
                    }
                    break;
                default: Console.WriteLine("에러 발생"); break;
            }
        } // SpecialEvent
    }// EventOccur
}// TextGame