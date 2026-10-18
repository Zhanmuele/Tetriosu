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
        private List<string> nextPieceTypes = new List<string>(5);
        private List<string> currentBag = new List<string>(14);
        private Random random = new Random();
        private string previousRolledPieceType = "";
        private Action bagGeneratorFunction;

        public BagGenerator(NewBagGenerationRules rule)
        {
            switch (rule)
            {
                case NewBagGenerationRules.SevenBag:
                    bagGeneratorFunction = generateSevenBag;
                    break;

                case NewBagGenerationRules.FullRandom:
                    bagGeneratorFunction = generateFullRandomBag;
                    break;

                case NewBagGenerationRules.ClassicRandom:
                    bagGeneratorFunction = generateClassicRandomBag;
                    break;

                default:
                    throw new InvalidEnumArgumentException($"{nameof(rule)} is not an valid NewBagGenerationRules rule");
            }
        }

        private void generateSevenBag()
        {
            currentBag.AddRange(DEFAULT_PIECE_TYPES);
            shuffle(currentBag);
        }

        private void generateFullRandomBag()
        {
            int x;

            // I believe seven prerolled pieces is a good compromise
            // to avoid invoking this function at every placement
            // and small stutters when called
            for (int i = 0; i < 7; i++)
            {
                x = random.Next(DEFAULT_PIECE_TYPES.Length);
                currentBag.Add(DEFAULT_PIECE_TYPES[x]);
            }
        }


        private void generateClassicRandomBag()
        {
            int x;

            // I believe seven prerolled pieces is a good compromise
            // to avoid invoking this function at every placement
            // and small stutters when called
            for (int i = 0; i < 7; i++)
            {
                x = random.Next(8);

                if (x == 7)
                {
                    x = random.Next(7);
                }
                else if (DEFAULT_PIECE_TYPES[x] == previousRolledPieceType)
                {
                    x = random.Next(7);
                }

                previousRolledPieceType = DEFAULT_PIECE_TYPES[x];
                currentBag.Add(DEFAULT_PIECE_TYPES[x]);
            }
        }

        public string GetNextPieceType()
        {
            string nextPieceType;

            while (nextPieceTypes.Count < 5)
            {
                if (currentBag.Count == 0)
                {
                    bagGeneratorFunction.Invoke();
                }

                nextPieceTypes.Add(currentBag[^1]);
                currentBag.RemoveAt(currentBag.Count - 1);
            }

            nextPieceType = nextPieceTypes[^1];
            nextPieceTypes.RemoveAt(nextPieceTypes.Count - 1);

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
