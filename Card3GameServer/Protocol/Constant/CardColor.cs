﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Constant
{
    /// <summary>
    /// 卡牌花色
    /// </summary>
    public class CardColor
    {
        public const int NONE = 0;
        //fixbug923 
        public const int CLUB = 1;//梅花
        public const int HEART = 2;//红桃
        public const int SPADE = 3;//黑桃
        public const int SQUARE = 4;//方片

        public static string GetString(int color)
        {
            switch (color)
            {
                case CLUB:
                    return "Club";
                case HEART:
                    return "Heart";
                case SPADE:
                    return "Spade";
                case SQUARE:
                    return "Square";
                default:
                    throw new Exception("不存在这样的花色");
            }
        }
        //public const int NONE = 0;
        //public const int TIAO = 1;//条
        //public const int TONG = 2;//筒
        //public const int WAN = 3;// 万

        //public static string GetString(int color)
        //{
        //    switch (color)
        //    {
        //        case TIAO:
        //            return "tiao";
        //        case TONG:
        //            return "tong";
        //        case WAN:
        //            return "w";
        //        default:
        //            throw new Exception("不存在这样的花");

        //    }
        //}
    }
}
