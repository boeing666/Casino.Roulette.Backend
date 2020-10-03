using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Models.Roulette;

namespace Casino.Roulette.Backend.Contracts.Settings
{
    public class Constants
    {
        public static long BettingTime { get; set; } = 15 * 1000;
        public static long AfterRoundTime { get; set; } = 3 * 1000;
        public static long BallBouncingTime { get; set; } = 5 * 1000;


        public static List<RouletteNum> RouletteNumbers { get; set; } = new List<RouletteNum>()
        {
            new RouletteNum() {Color = Color.Green, Number = 0},
            new RouletteNum() {Color = Color.Red, Number = 1},
            new RouletteNum() {Color = Color.Black, Number = 2},
            new RouletteNum() {Color = Color.Red, Number = 3},
            new RouletteNum() {Color = Color.Black, Number = 4},
            new RouletteNum() {Color = Color.Red, Number = 5},
            new RouletteNum() {Color = Color.Black, Number = 6},
            new RouletteNum() {Color = Color.Red, Number = 7},
            new RouletteNum() {Color = Color.Black, Number = 8},
            new RouletteNum() {Color = Color.Red, Number = 9},
            new RouletteNum() {Color = Color.Black, Number = 10},
            new RouletteNum() {Color = Color.Black, Number = 11},
            new RouletteNum() {Color = Color.Red, Number = 12},
            new RouletteNum() {Color = Color.Black, Number = 13},
            new RouletteNum() {Color = Color.Red, Number = 14},
            new RouletteNum() {Color = Color.Black, Number = 15},
            new RouletteNum() {Color = Color.Red, Number = 16},
            new RouletteNum() {Color = Color.Black, Number = 17},
            new RouletteNum() {Color = Color.Red, Number = 18},
            new RouletteNum() {Color = Color.Red, Number = 19},
            new RouletteNum() {Color = Color.Black, Number = 20},
            new RouletteNum() {Color = Color.Red, Number = 21},
            new RouletteNum() {Color = Color.Black, Number = 22},
            new RouletteNum() {Color = Color.Red, Number = 23},
            new RouletteNum() {Color = Color.Black, Number = 24},
            new RouletteNum() {Color = Color.Red, Number = 25},
            new RouletteNum() {Color = Color.Black, Number = 26},
            new RouletteNum() {Color = Color.Red, Number = 27},
            new RouletteNum() {Color = Color.Black, Number = 28},
            new RouletteNum() {Color = Color.Black, Number = 29},
            new RouletteNum() {Color = Color.Red, Number = 30},
            new RouletteNum() {Color = Color.Black, Number = 31},
            new RouletteNum() {Color = Color.Red, Number = 32},
            new RouletteNum() {Color = Color.Black, Number = 33},
            new RouletteNum() {Color = Color.Red, Number = 34},
            new RouletteNum() {Color = Color.Black, Number = 35},
            new RouletteNum() {Color = Color.Red, Number = 36},
        };


        public static List<BetIndex> BetIndexes { get; set; } = new List<BetIndex>()
        {
            new BetIndex() {Index = 37, Multiplier = 3, Label = "1 St 12"},
            new BetIndex() {Index = 38, Multiplier = 3, Label = "2 Nd 12"},
            new BetIndex() {Index = 39, Multiplier = 3, Label = "3 Rd 12"},
            new BetIndex() {Index = 40, Multiplier = 2, Label = "1 To 18"},
            new BetIndex() {Index = 41, Multiplier = 2, Label = "19 To 36"},
            new BetIndex() {Index = 42, Multiplier = 2, Label = "EVEN"},
            new BetIndex() {Index = 43, Multiplier = 2, Label = "ODD"},
            new BetIndex() {Index = 44, Multiplier = 2, Label = "RED"},
            new BetIndex() {Index = 45, Multiplier = 2, Label = "BLACK"}
        };
    }
}