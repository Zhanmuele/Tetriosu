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
        public static List<string> NextPieceTypes { get; private set; } = new List<string>();
        private List<string> currentBag = new List<string>();
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

            bagGeneratorFunction.Invoke();
            fillNextPiecesBar();
        }

        private void generateSevenBag()
        {
            currentBag.AddRange(DEFAULT_PIECE_TYPES);
            shuffle(currentBag);
        }

        private void generateFullRandomBag()
        {
            int x;

            for (int i = 0; i < 7; i++)
            {
                x = random.Next(DEFAULT_PIECE_TYPES.Length);
                currentBag.Add(DEFAULT_PIECE_TYPES[x]);
            }
        }

        private void generateClassicRandomBag()
        {
            int x;

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
            string nextPieceType = NextPieceTypes[0];
            NextPieceTypes.RemoveAt(0);
            fillNextPiecesBar();

            return nextPieceType;
        }

        private void fillNextPiecesBar()
        {
            while (NextPieceTypes.Count < 5)
            {
                if (currentBag.Count == 0)
                {
                    bagGeneratorFunction.Invoke();
                }

                NextPieceTypes.Add(currentBag[0]);
                currentBag.RemoveAt(0);
            }
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
