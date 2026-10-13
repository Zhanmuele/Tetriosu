using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using Tetriosu.Game.Gameplay.GameRuleEnum;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class GameFunctions : Component
    {
        public string HeldPieceType;
        public bool IsSoftDropping = false;
        public bool HasSwapped = false;
        public List<string> NextPieceTypes = new List<string>(5);
        public List<string> CurrentBag = new List<string>(14);
        private Random rng = new Random();

        public void GenerateNewBag(NewBagGenerationRules rule)
        {
            switch (rule)
            {
                case NewBagGenerationRules.SevenBag:
                {
                    CurrentBag = [.. GameSetupValues.DEFAULT_PIECE_TYPES];
                    shuffle<List<string>>(CurrentBag);
                    break;
                }
            }
        }

        public string GetNextPieceType(NewBagGenerationRules rule)
        {
            string nextPieceType;

            switch (rule)
            {
                case NewBagGenerationRules.SevenBag:
                {
                    if (CurrentBag.Count == 0)
                    {
                        GenerateNewBag(rule);
                    }

                    while (NextPieceTypes.Count < 5)
                    {
                        NextPieceTypes.Add(CurrentBag[0]);
                        CurrentBag.RemoveAt(0);
                    }

                    nextPieceType = NextPieceTypes[0];
                    NextPieceTypes.RemoveAt(0);

                    break;

                }
                default: throw new ArgumentOutOfRangeException(nameof(rule), rule, "Invalid bag generation rule");
            }

            return nextPieceType;
        }

        private void shuffle<T>( List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
