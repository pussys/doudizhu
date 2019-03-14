using Protocol.Dto.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Constant
{
    /// <summary>
    /// 卡牌权值
    /// </summary>
    public class CardWeight
    {
        public const int THREE = 3;
        public const int FOUR = 4;
        public const int FIVE = 5;
        public const int SIX = 6;
        public const int SEVEN = 7;
        public const int EIGHT = 8;
        public const int NINE = 9;
        public const int TEN = 10;

        public const int JACK = 11;
        public const int QUEEN = 12;
        public const int KING = 13;

        public const int ONE = 14;
        public const int TWO = 15;

        public const int SJOKER = 16;
        public const int LJOKER = 17;

        //public const int ONE = 1;
        //public const int TWO = 2;
        //public const int THREE = 3;
        //public const int FOUR = 4;
        //public const int FIVE = 5;
        //public const int SIX = 6;
        //public const int SEVEN = 7;
        //public const int EIGHT = 8;
        //public const int NINE = 9;

        //public const int BAI = 10;
        //public const int BEI = 11;
        //public const int DONG = 12;
        //public const int NAN = 13;
        //public const int XI = 14;
        //public const int ZHONG = 15;
        //public const int FA = 15;


        public static string GetString(int weight)
        {
            switch (weight)
            {
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                case 8:
                    return "Eight";
                case 9:
                    return "Nine";
                case 10:
                    return "Ten";
                case 11:
                    return "Jack";
                case 12:
                    return "Queen";
                case 13:
                    return "King";
                case 14:
                    return "One";
                case 15:
                    return "Two";
                case 16:
                    return "SJoker";
                case 17:
                    return "LJoker";
                default:
                    throw new Exception("不存在这样的权值");
            }
            //switch (weight)
            //{
            //    case 1:
            //        return "_1";
            //    case 2:
            //        return "_2";
            //    case 3:
            //        return "_3";
            //    case 4:
            //        return "_4";
            //    case 5:
            //        return "_5";
            //    case 6:
            //        return "_6";
            //    case 7:
            //        return "_7";
            //    case 8:
            //        return "_8";
            //    case 9:
            //        return "_9";
            //    case 10:
            //        return "bai";
            //    case 11:
            //        return "bei";
            //    case 12:
            //        return "dong";
            //    case 13:
            //        return "nan";
            //    case 14:
            //        return "xi";
            //    case 15:
            //        return "zhong";
            //    case 16:
            //        return "fa";
            //    default:
            //        throw new Exception("不存在这样的权值");
            //}
        }

        /// <summary>
        /// 获取卡牌的权值
        /// </summary>
        /// <param name="cardList">选中的卡牌</param>
        /// <param name="cardType">出牌类型</param>
        /// <returns></returns>
        public static int GetWeight(List<CardDto> cardList, int cardType)
        {
            int totalWeight = 0;
            if (cardType == CardType.THREE_ONE || cardType == CardType.THREE_TWO || cardType == CardType.FEIJI)
            {
                //如果是 三代一 或者说 三代二或者飞机
                // 3335  4443   5333  3335  3353
                for (int i = 0; i < cardList.Count - 2; i++)
                {
                    if (cardList[i].Weight == cardList[i + 1].Weight && cardList[i].Weight == cardList[i + 2].Weight)
                    {
                        totalWeight += (cardList[i].Weight * 3);
                    }
                }
            }
            else if (cardType == CardType.SIDAIER)
            {
                //444423  
                //234444  
                //233334  
                for (int i = 0; i < cardList.Count - 3; i++)
                {
                    if (cardList[i + 2].Weight == cardList[i + 3].Weight)
                    {
                        //totalWeight = cardList[i + 2].Weight + cardList[i + 3].Weight;
                        totalWeight += (cardList[i + 2].Weight * 4);
                    }

                }
            }
            else if (cardType == CardType.SIDAIDUI)
            {
                //44445566
                //22334444
                //22333344
                for (int i = 0; i < cardList.Count - 7; i++)
                {
                    if (cardList[i + 2].Weight == cardList[i + 3].Weight)
                    {
                        if (cardList[i + 4].Weight == cardList[i + 5].Weight && cardList[i + 5].Weight == cardList[i + 6].Weight &&
                            cardList[i + 6].Weight == cardList[i + 7].Weight)
                        {
                            totalWeight += (cardList[i + 4].Weight * 4);
                        }
                        else
                        {
                            totalWeight += (cardList[i + 2].Weight * 4);
                        }

                    }

                }

            }
            else if (cardType == CardType.FEIJIDAIER)
            {
                ///3334445566 012345
                ///3344555666 456789
                ///3344455566 234567
                for (int i = 0; i < cardList.Count - 9; i++)
                {
                    if (cardList[i + 5].Weight == cardList[i + 6].Weight && cardList[8].Weight == cardList[9].Weight)
                    {
                        if (cardList[i].Weight == cardList[i + 1].Weight && cardList[i + 2].Weight == cardList[i + 3].Weight)
                        {
                            totalWeight += cardList[i + 4].Weight + cardList[i + 5].Weight + cardList[i + 6].Weight + cardList[i + 7].Weight
                                + cardList[i + 8].Weight + cardList[i + 9].Weight;
                        }
                        else
                        {
                            totalWeight += cardList[i + 2].Weight + cardList[i + 3].Weight + cardList[i + 4].Weight + cardList[i + 5].Weight
                                + cardList[i + 6].Weight + cardList[i + 7].Weight;
                        }
                    }
                    else
                    {
                        totalWeight += cardList[i].Weight + cardList[i + 1].Weight + cardList[i + 2].Weight + cardList[i + 3].Weight
                                + cardList[i + 4].Weight + cardList[i + 5].Weight;
                    }
                }
            }

            else
            {
                for (int i = 0; i < cardList.Count; i++)
                {
                    totalWeight += cardList[i].Weight;
                }
            }
            return totalWeight;
        }

    }
}
