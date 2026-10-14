using System;
using System.Collections.Generic;
using System.ComponentModel;
using osu.Framework.Graphics;
using Tetriosu.Game.Gameplay.GameRuleEnum;

namespace Tetriosu.Game.Gameplay.Components.BagGenerator
{
    public partial class BagGenerator : Drawable
    {
        public static readonly string[] DEFAULT_PIECE_TYPES = new string[7] { "Z", "L", "O", "S", "I", "J", "T" };
        public List<string> NextPieceTypes = new List<string>(5);
        public List<string> CurrentBag = new List<string>();
        private Random random = new Random();
        public Action BagGeneratorFunction;

        public BagGenerator(NewBagGenerationRules rule)
        {
            switch (rule)
            {
                case NewBagGenerationRules.SevenBag:
                    BagGeneratorFunction = generateSevenBag;
                    break;

                case NewBagGenerationRules.FullRandom:
                    BagGeneratorFunction = generateFullRandomBag;
                    break;

                case NewBagGenerationRules.ClassicRandom:

                default:
                    throw new InvalidEnumArgumentException($"{nameof(rule)} is not an valid NewBagGenerationRules rule");
            }
        }

        private void generateSevenBag()
        {
            CurrentBag = [.. DEFAULT_PIECE_TYPES];
            shuffle(CurrentBag);
        }

        private void generateFullRandomBag()
        {
            int i;

            for (int x = 0; x < 7; i++)
            {
                i = random.Next(DEFAULT_PIECE_TYPES.Length);
                CurrentBag.Add(DEFAULT_PIECE_TYPES[i]);
            }
        }

        public string GetNextPieceType()
        {
            string nextPieceType;
 
            while (NextPieceTypes.Count < 5)
            {
                if (CurrentBag.Count == 0)
                {
                    BagGeneratorFunction.Invoke();
                }

                NextPieceTypes.Add(CurrentBag[0]);
                CurrentBag.RemoveAt(0);
            }

            nextPieceType = NextPieceTypes[0];
            NextPieceTypes.RemoveAt(0);

            return nextPieceType;
        }

        private void shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
