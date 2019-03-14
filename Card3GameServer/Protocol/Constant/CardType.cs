using Protocol.Dto.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Constant
{
    /// <summary>
    /// 卡牌类型
    /// </summary>
    public class CardType
    {
        public const int NONE = 0;
        public const int SINGLE = 1;//单排
        public const int DOUBLE = 2;//对儿
        public const int STRAIGHT = 3;//顺子
        public const int DOUBLE_STRAIGHT = 4;//双顺 44 55 66
        public const int TRIPLE_STRAIGHT = 5;//三顺 444 555 666
        public const int THREE = 6;//三不带  444
        public const int THREE_ONE = 7;//三代一  444 5
        public const int THREE_TWO = 8;//三代二 444 55
        public const int BOOM = 9;//炸弹
        public const int JOKER_BOOM = 10;//王炸
        public const int FEIJI = 11;//三顺加同数量的单牌(带单)33344456
        public const int SIDAIER = 12;//四带二(带单)333345
        public const int SIDAIDUI = 13;//四带两对33334455
        public const int FEIJIDAIER = 14;//三顺加同数量的对牌(带双)3334445566

        /// <summary>
        /// 是否是单牌
        /// </summary>
        /// <param name="cards">选择的手牌</param>
        /// <returns></returns>
        public static bool IsSingle(List<CardDto> cards)
        {
            return cards.Count == 1;
        }

        /// <summary>
        /// 判断是否是对儿
        /// </summary>
        /// <param name="cards">选择的手牌</param>
        /// <returns></returns>
        public static bool IsDouble(List<CardDto> cards)
        {
            if (cards.Count == 2)
            {
                if (cards[0].Weight == cards[1].Weight)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是顺子
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsStraight(List<CardDto> cards)
        {
            if (cards.Count < 5 || cards.Count > 12)
                return false;
            //345678910JQKA
            // 34567   45679  JQKA2
            for (int i = 0; i < cards.Count - 1; i++)
            {
                int tempWeight = cards[i].Weight;
                if (cards[i + 1].Weight - tempWeight != 1)
                    return false;
                //不能超过A
                if (tempWeight > CardWeight.ONE || cards[i + 1].Weight > CardWeight.ONE)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 是否是双顺
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsDoubleStraight(List<CardDto> cards)
        {
            //334455 
            //33445566
            if (cards.Count < 6 || cards.Count % 2 != 0)
                return false;

            for (int i = 0; i < cards.Count - 5; i += 2)
            {
                if (cards[i].Weight != cards[i + 1].Weight)
                    return false;
                if (cards[i + 2].Weight != cards[i + 3].Weight)
                {
                    return false;
                }
                if (cards[i + 4].Weight != cards[i + 5].Weight)
                {
                    return false;
                }
                if (cards[i + 4].Weight - cards[i + 2].Weight != 1)
                {
                    return false;
                }
                if (cards[i + 2].Weight - cards[i].Weight != 1)
                    return false;
                //不能超过A
                if (cards[i].Weight > CardWeight.ONE || cards[i + 2].Weight > CardWeight.ONE)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 是否是三顺
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsTripleStraight(List<CardDto> cards)
        {
            //333 444 555
            // 33344456  333444 66 77
            if (cards.Count < 6 || cards.Count % 3 != 0)
                return false;

            for (int i = 0; i < cards.Count - 3; i += 3)
            {
                if (cards[i].Weight != cards[i + 1].Weight)
                    return false;
                if (cards[i + 2].Weight != cards[i + 1].Weight)
                    return false;
                if (cards[i].Weight != cards[i + 2].Weight)
                    return false;

                if (cards[i + 3].Weight - cards[i].Weight != 1)
                    return false;
                //不能超过A
                if (cards[i].Weight > CardWeight.ONE || cards[i + 3].Weight > CardWeight.ONE)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 是否是三不带
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThree(List<CardDto> cards)
        {
            //333
            if (cards.Count != 3)
                return false;
            if (cards[0].Weight != cards[1].Weight)
                return false;
            if (cards[2].Weight != cards[1].Weight)
                return false;
            if (cards[0].Weight != cards[2].Weight)
                return false;

            return true;
        }

        /// <summary>
        /// 是否是三带一
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThreeAndOne(List<CardDto> cards)
        {
            if (cards.Count != 4)
                return false;

            //5333 3335
            if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight)
                return true;
            else if (cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight)
                return true;

            return false;
        }

        /// <summary>
        /// 是否是三带二
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThreeAndTwo(List<CardDto> cards)
        {
            if (cards.Count != 5)
                return false;
            //33355 55333
            if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight)
            {
                if (cards[3].Weight == cards[4].Weight)
                    return true;
            }
            else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight)
            {
                if (cards[0].Weight == cards[1].Weight)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 判断是否是炸弹
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsBoom(List<CardDto> cards)
        {
            if (cards.Count != 4)
                return false;
            // 0000
            if (cards[0].Weight != cards[1].Weight)
                return false;
            if (cards[1].Weight != cards[2].Weight)
                return false;
            if (cards[2].Weight != cards[3].Weight)
                return false;

            return true;
        }

        /// <summary>
        /// 判断是不是王炸
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsJokerBoom(List<CardDto> cards)
        {
            if (cards.Count != 2)

                return false;

            if (cards[0].Weight == CardWeight.SJOKER && cards[1].Weight == CardWeight.LJOKER)
                return true;

            else if (cards[0].Weight == CardWeight.LJOKER && cards[1].Weight == CardWeight.SJOKER)

                return true;

            return false;
        }
        /// <summary>
        /// 飞机带翅膀 
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsFeiJi(List<CardDto> cards)
        {
            //33344456    333444555678   33344455566677788999
            //34445556    345556667778   233344455566677789JQ  1 4
            //            355566677789   234445556667778889JQ  2 3
            //                           1234445556667778889J  3 2
            //                          1234555666777888999J  1 4
            //34555666    345666777888   34567888999101010JJJQQQ
            if (cards.Count < 6 || cards.Count % 4 != 0)
            {
                return false;

            }

            if (cards.Count == 8)
            {
                if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight)
                {
                    if (cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
                    {
                        if (cards[0].Weight < CardWeight.ONE && cards[3].Weight - cards[0].Weight == 1)
                        {
                            return true;

                        }

                    }
                }
                else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight)
                {
                    if (cards[5].Weight == cards[6].Weight && cards[6].Weight == cards[7].Weight)
                    {
                        if (cards[2].Weight < CardWeight.ONE && cards[5].Weight - cards[2].Weight == 1)
                        {
                            return true;

                        }
                    }
                }
                else if (cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight)
                {
                    if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight)
                    {
                        if (cards[4].Weight - cards[3].Weight == 1 && cards[3].Weight < CardWeight.ONE)
                        {
                            return true;

                        }
                    }
                }
            }
            if (cards.Count == 12)
            {
                if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight)
                {
                    if (cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
                    {
                        if (cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight)
                        {
                            if (cards[5].Weight < CardWeight.ONE && cards[6].Weight - cards[5].Weight == 1 && cards[3].Weight - cards[2].Weight == 1)
                            {
                                //333444555678
                                return true;
                            }

                        }
                    }
                }
                else if (cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
                {
                    if (cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight)
                    {
                        if (cards[9].Weight == cards[10].Weight && cards[10].Weight == cards[11].Weight)
                        {
                            if (cards[8].Weight < CardWeight.ONE && cards[11].Weight - cards[6].Weight == 1 && cards[6].Weight - cards[5].Weight == 1)
                                //345666777888
                                return true;
                        }
                    }
                }
                //355566677789
                else if (cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight)
                {
                    if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight)
                    {
                        if (cards[7].Weight == cards[8].Weight && cards[8].Weight == cards[9].Weight)
                        {
                            if (cards[4].Weight < CardWeight.ONE && cards[7].Weight - cards[6].Weight == 1 && cards[6].Weight - cards[3].Weight == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
                //345556667778
                else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight)
                {
                    if (cards[5].Weight == cards[6].Weight && cards[6].Weight == cards[7].Weight)
                    {
                        if (cards[8].Weight == cards[9].Weight && cards[9].Weight == cards[10].Weight)
                        {
                            if (cards[5].Weight < CardWeight.ONE && cards[8].Weight - cards[5].Weight == 1 && cards[5].Weight - cards[2].Weight == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            //34567888999101010JJJQQQ
            if (cards.Count == 20)
            {
                if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight)
                {
                    if (cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
                    {
                        if (cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight)
                        {
                            if (cards[9].Weight == cards[10].Weight && cards[10].Weight == cards[11].Weight)
                            {
                                if (cards[12].Weight == cards[13].Weight && cards[13].Weight == cards[14].Weight)
                                {
                                    if (cards[9].Weight < CardWeight.ONE && cards[12].Weight - cards[9].Weight == 1 && cards[9].Weight - cards[6].Weight == 1
                                        && cards[6].Weight - cards[3].Weight == 1 && cards[3].Weight - cards[0].Weight == 1)
                                        //33344455566677788999
                                        return true;
                                }
                            }
                        }
                    }
                }
                else if (cards[5].Weight == cards[6].Weight && cards[6].Weight == cards[7].Weight)
                {
                    if (cards[8].Weight == cards[9].Weight && cards[9].Weight == cards[10].Weight)
                    {
                        if (cards[11].Weight == cards[12].Weight && cards[12].Weight == cards[13].Weight)
                        {
                            if (cards[14].Weight == cards[15].Weight && cards[15].Weight == cards[16].Weight)
                            {
                                if (cards[17].Weight == cards[18].Weight && cards[18].Weight == cards[19].Weight)
                                {
                                    if (cards[14].Weight < CardWeight.ONE && cards[17].Weight - cards[14].Weight == 1 && cards[14].Weight - cards[11].Weight == 1
                                        && cards[11].Weight - cards[8].Weight == 1 && cards[8].Weight - cards[5].Weight == 1)
                                        //34567888999101010JJJQQQ
                                        return true;
                                }
                            }
                        }
                    }
                }
                //0123456789!@#$%^&*()
                //233344455566677789JQ  1 4
                else if (cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight)
                {
                    if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight)
                    {
                        if (cards[7].Weight == cards[8].Weight && cards[8].Weight == cards[9].Weight)
                        {
                            if (cards[10].Weight == cards[11].Weight && cards[11].Weight == cards[12].Weight)
                            {
                                if (cards[13].Weight == cards[14].Weight && cards[14].Weight == cards[15].Weight)
                                {
                                    if (cards[10].Weight < CardWeight.ONE && cards[13].Weight - cards[10].Weight == 1 && cards[10].Weight - cards[7].Weight == 1
                                        && cards[7].Weight - cards[4].Weight == 1 && cards[4].Weight - cards[1].Weight == 1)
                                        return true;
                                }
                            }
                        }
                    }
                }
                //0123456789!@#$%^&*()
                //234445556667778889JQ  2 3
                else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight)
                {
                    if (cards[5].Weight == cards[6].Weight && cards[6].Weight == cards[7].Weight)
                    {
                        if (cards[8].Weight == cards[9].Weight && cards[9].Weight == cards[10].Weight)
                        {
                            if (cards[11].Weight == cards[12].Weight && cards[12].Weight == cards[13].Weight)
                            {
                                if (cards[14].Weight == cards[15].Weight && cards[15].Weight == cards[16].Weight)
                                {
                                    if (cards[11].Weight < CardWeight.ONE && cards[14].Weight - cards[11].Weight == 1 && cards[11].Weight - cards[8].Weight == 1
                                        && cards[8].Weight - cards[5].Weight == 1 && cards[5].Weight - cards[2].Weight == 1)
                                        return true;
                                }
                            }
                        }
                    }
                }
                // 0123456789!@#$%^&*()
                // 1234445556667778889J  3 2
                else if (cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
                {
                    if (cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight)
                    {
                        if (cards[9].Weight == cards[10].Weight && cards[10].Weight == cards[11].Weight)
                        {
                            if (cards[12].Weight == cards[13].Weight && cards[13].Weight == cards[14].Weight)
                            {
                                if (cards[15].Weight == cards[16].Weight && cards[16].Weight == cards[17].Weight)
                                {
                                    if (cards[12].Weight < CardWeight.ONE && cards[15].Weight - cards[12].Weight == 1 && cards[12].Weight - cards[9].Weight == 1
                                        && cards[9].Weight - cards[6].Weight == 1 && cards[6].Weight - cards[3].Weight == 1)
                                        return true;
                                }
                            }
                        }
                    }
                }

                //0123456789!@#$%^&*()
                //1234555666777888999J  1 4
                else if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight)
                {
                    if (cards[7].Weight == cards[8].Weight && cards[8].Weight == cards[9].Weight)
                    {
                        if (cards[10].Weight == cards[11].Weight && cards[11].Weight == cards[12].Weight)
                        {
                            if (cards[13].Weight == cards[14].Weight && cards[14].Weight == cards[15].Weight)
                            {
                                if (cards[16].Weight == cards[17].Weight && cards[17].Weight == cards[18].Weight)
                                {
                                    if (cards[13].Weight < CardWeight.ONE && cards[16].Weight - cards[13].Weight == 1 && cards[13].Weight - cards[10].Weight == 1
                                        && cards[10].Weight - cards[7].Weight == 1 && cards[7].Weight - cards[4].Weight == 1)
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 飞机带对
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsFeiJiDui(List<CardDto> cards)
        {

            if (cards.Count == 10)
            {
                if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight && cards[3].Weight == cards[4].Weight
                    && cards[4].Weight == cards[5].Weight && cards[3].Weight - cards[2].Weight == 1)
                {
                    if (cards[6].Weight == cards[7].Weight && cards[8].Weight == cards[9].Weight)
                    {
                        if (cards[0].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight && cards[7].Weight == cards[8].Weight
                    && cards[8].Weight == cards[9].Weight && cards[9].Weight - cards[4].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight)
                    {
                        if (cards[4].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight && cards[5].Weight == cards[6].Weight
                   && cards[6].Weight == cards[7].Weight && cards[7].Weight - cards[2].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[8].Weight == cards[9].Weight)
                    {
                        if (cards[2].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
            }
            //3334445566  333444555667788  3334445556667788991010  0 4
            //3344555666  334455666777888  33445566777888999101010 4 0
            //3344455566  334455566677788  3344455566677788991010  1 3
            //            334445556667788  3344555666777888991010  2 2
            //                             3344556667778889991010  3 1
            if (cards.Count == 15)
            {
                if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight && cards[3].Weight == cards[4].Weight
                   && cards[4].Weight == cards[5].Weight && cards[6].Weight == cards[7].Weight
                   && cards[7].Weight == cards[8].Weight && cards[3].Weight - cards[2].Weight == 1 && cards[8].Weight - cards[3].Weight == 1)
                {
                    if (cards[9].Weight == cards[10].Weight && cards[11].Weight == cards[12].Weight && cards[13].Weight == cards[14].Weight)
                    {
                        if (cards[3].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight && cards[9].Weight == cards[10].Weight
                   && cards[10].Weight == cards[11].Weight && cards[12].Weight == cards[13].Weight
                   && cards[13].Weight == cards[14].Weight && cards[14].Weight - cards[11].Weight == 1 && cards[11].Weight - cards[6].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight && cards[13].Weight == cards[14].Weight)
                    {
                        if (cards[7].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight && cards[7].Weight == cards[8].Weight
                           && cards[8].Weight == cards[9].Weight && cards[10].Weight == cards[11].Weight
                           && cards[11].Weight == cards[12].Weight && cards[12].Weight - cards[9].Weight == 1 && cards[9].Weight - cards[4].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight && cards[4].Weight == cards[5].Weight)
                    {
                        if (cards[11].Weight < CardWeight.ONE)
                        {
                            return true;

                        }
                    }
                }
                //334445556667788
                else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight && cards[5].Weight == cards[6].Weight
                          && cards[6].Weight == cards[7].Weight && cards[8].Weight == cards[9].Weight
                          && cards[9].Weight == cards[10].Weight && cards[10].Weight - cards[7].Weight == 1 && cards[7].Weight - cards[2].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight && cards[4].Weight == cards[5].Weight)
                    {
                        if (cards[11].Weight < CardWeight.ONE)
                        {
                            return true;

                        }
                    }
                }
            }
            //0123456789!@#$%^&*_=
            //3334445556667788991010
            if (cards.Count == 20)
            {
                if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight && cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight
                    && cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight && cards[9].Weight == cards[10].Weight && cards[10].Weight == cards[11].Weight
                    && cards[11].Weight - cards[6].Weight == 1 && cards[6].Weight - cards[5].Weight == 1 && cards[5].Weight - cards[0].Weight == 1)
                {
                    if (cards[12].Weight == cards[13].Weight && cards[14].Weight == cards[15].Weight && cards[16].Weight == cards[17].Weight && cards[18].Weight == cards[19].Weight)
                    {
                        if (cards[6].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                //33445566777888999101010
                else if (cards[8].Weight == cards[9].Weight && cards[9].Weight == cards[10].Weight && cards[11].Weight == cards[12].Weight && cards[12].Weight == cards[13].Weight
                     && cards[14].Weight == cards[15].Weight && cards[15].Weight == cards[16].Weight && cards[17].Weight == cards[18].Weight && cards[18].Weight == cards[19].Weight
                     && cards[19].Weight - cards[16].Weight == 1 && cards[16].Weight - cards[13].Weight == 1 && cards[13].Weight - cards[10].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight && cards[4].Weight == cards[5].Weight && cards[6].Weight == cards[7].Weight)
                    {
                        if (cards[16].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight && cards[5].Weight == cards[6].Weight && cards[6].Weight == cards[7].Weight
                 && cards[8].Weight == cards[9].Weight && cards[9].Weight == cards[10].Weight && cards[11].Weight == cards[12].Weight && cards[12].Weight == cards[13].Weight
                 && cards[13].Weight - cards[10].Weight == 1 && cards[10].Weight - cards[7].Weight == 1 && cards[7].Weight - cards[4].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[14].Weight == cards[15].Weight && cards[16].Weight == cards[17].Weight && cards[18].Weight == cards[19].Weight)
                    {
                        if (cards[10].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight && cards[7].Weight == cards[8].Weight && cards[8].Weight == cards[9].Weight
                && cards[10].Weight == cards[11].Weight && cards[11].Weight == cards[12].Weight && cards[13].Weight == cards[14].Weight && cards[14].Weight == cards[15].Weight
                && cards[15].Weight - cards[12].Weight == 1 && cards[12].Weight - cards[9].Weight == 1 && cards[9].Weight - cards[6].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight && cards[16].Weight == cards[17].Weight && cards[18].Weight == cards[19].Weight)
                    {
                        if (cards[12].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
                else if (cards[6].Weight == cards[7].Weight && cards[7].Weight == cards[8].Weight && cards[9].Weight == cards[10].Weight && cards[10].Weight == cards[11].Weight
               && cards[12].Weight == cards[13].Weight && cards[13].Weight == cards[14].Weight && cards[15].Weight == cards[16].Weight && cards[16].Weight == cards[17].Weight
               && cards[17].Weight - cards[14].Weight == 1 && cards[14].Weight - cards[11].Weight == 1 && cards[11].Weight - cards[8].Weight == 1)
                {
                    if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight && cards[4].Weight == cards[5].Weight && cards[18].Weight == cards[19].Weight)
                    {
                        if (cards[14].Weight < CardWeight.ONE)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 四带二
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool SiDaiEr(List<CardDto> cards)
        {
            //333345   
            //234444  
            //344445 
            if (cards.Count != 6)
            {
                return false;
            }
            if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight)
            {
                if (cards[4].Weight != cards[0].Weight && cards[5].Weight != cards[0].Weight)
                {
                    return true;
                }

            }
            else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
            {
                if (cards[0].Weight != cards[2].Weight && cards[1].Weight != cards[2].Weight)
                {
                    return true;
                }


            }
            else if (cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight)
            {
                if (cards[0].Weight != cards[2].Weight && cards[1].Weight != cards[2].Weight)
                {
                    return true;
                }

            }
            return false;
        }
        public static bool SiDaiDui(List<CardDto> cards)
        {
            //33334455  33444455  22334444
            if (cards.Count != 8)
            {
                return false;
            }
            if (cards[0].Weight == cards[1].Weight && cards[1].Weight == cards[2].Weight && cards[2].Weight == cards[3].Weight)
            {
                if (cards[4].Weight == cards[5].Weight && cards[6].Weight == cards[7].Weight)
                {
                    if (cards[4].Weight != cards[0].Weight && cards[4].Weight != cards[6].Weight && cards[0].Weight != cards[6].Weight)
                    {
                        return true;

                    }

                }
            }
            else if (cards[2].Weight == cards[3].Weight && cards[3].Weight == cards[4].Weight && cards[4].Weight == cards[5].Weight)
            {
                if (cards[0].Weight == cards[1].Weight && cards[6].Weight == cards[7].Weight)
                {
                    if (cards[0].Weight != cards[1].Weight && cards[0].Weight != cards[7].Weight && cards[2].Weight != cards[7].Weight)
                    {
                        return true;

                    }

                }
            }
            else if (cards[4].Weight == cards[5].Weight && cards[5].Weight == cards[6].Weight && cards[6].Weight == cards[7].Weight)
            {
                if (cards[0].Weight == cards[1].Weight && cards[2].Weight == cards[3].Weight)
                {
                    if (cards[0].Weight != cards[2].Weight && cards[0].Weight != cards[4].Weight && cards[2].Weight != cards[4].Weight)
                    {
                        return true;

                    }

                }
            }
            return false;
        }
        /// <summary>
        /// 获取卡牌类型
        /// </summary>
        /// <param name="cardList">要出的牌</param>
        public static int GetCardType(List<CardDto> cardList)
        {
            int cardType = CardType.NONE;

            switch (cardList.Count)
            {
                case 1:
                    if (IsSingle(cardList))
                    {
                        cardType = CardType.SINGLE;
                    }
                    break;
                case 2:
                    if (IsDouble(cardList))
                    {
                        cardType = CardType.DOUBLE;
                    }
                    else if (IsJokerBoom(cardList))
                    {
                        cardType = CardType.JOKER_BOOM;
                    }
                    break;
                case 3:
                    if (IsThree(cardList))
                    {
                        cardType = CardType.THREE;
                    }
                    break;
                case 4:
                    if (IsBoom(cardList))
                    {
                        cardType = CardType.BOOM;
                    }
                    else if (IsThreeAndOne(cardList))
                    {
                        cardType = CardType.THREE_ONE;
                    }
                    break;
                case 5:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsThreeAndTwo(cardList))
                    {
                        cardType = CardType.THREE_TWO;
                    }
                    break;
                case 6:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
                    else if (SiDaiEr(cardList))
                    {
                        cardType = CardType.SIDAIER;
                    }
                    break;
                case 7:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    break;
                case 8:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    else if (IsFeiJi(cardList))
                    {
                        cardType = CardType.FEIJI;
                    }
                    else if (SiDaiDui(cardList))
                    {
                        cardType = CardType.SIDAIDUI;
                    }
                    break;
                case 9:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    //777 888 999 
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
                    break;
                case 10:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    else if (IsFeiJiDui(cardList))
                    {

                        cardType = CardType.FEIJIDAIER;
                    }
                    break;
                case 11:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    break;
                case 12:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    // 444 555 666 777
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
                    else if (IsFeiJi(cardList))
                    {
                        cardType = CardType.FEIJI;
                    }
                    break;
                case 13:
                    //345678910JQKA
                    break;
                case 14:
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    break;
                case 15:
                    if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
                    else if (IsFeiJiDui(cardList))
                    {
                        cardType = CardType.FEIJIDAIER;
                    }
                    break;
                case 16:
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    break;
                case 17:
                    break;
                case 18:
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    // 444 555 666 777 888 999 
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
                    break;
                case 19:
                    break;
                case 20:
                    //33 44 55 66 77 88 99 1010 JJ QQ KK AA
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    else if (IsFeiJi(cardList))
                    {
                        cardType = CardType.FEIJI;
                    }
                    else if (IsFeiJiDui(cardList))
                    {
                        cardType = CardType.FEIJIDAIER;
                    }
                    break;
                default:
                    break;
            }

            return cardType;
        }

    }
}
