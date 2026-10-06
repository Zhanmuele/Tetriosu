using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Utils;
using Tetriosu.Game.Gameplay.GameRuleEnum;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class GameFunctions : Component
    {
        public char HeldPieceType;
        public bool IsSoftDropping = false;
        public bool HasSwapped = false;
        public BlockPiece?[,] PlayfieldData = new BlockPiece?[GameSetup.ROWS, GameSetup.COLUMS];
        public List<char> NextPieceTypes = new List<char>();
        public List<char> CurrentBag = new List<char>();
        private Random rng = new Random();

        public void GenerateNewBag(NewBagGenerationRules rule)
        {
            switch (rule)
            {
                case NewBagGenerationRules.SevenBag:
                {
                    CurrentBag = new List<char>(GameSetup.PIECE_TYPES);
                    shuffle<List<char>>(CurrentBag);
                    break;
                };
            }
        }

        public char GetNextPieceType(NewBagGenerationRules rule)
        {
            char nextPieceType;
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

                };
                default: throw new ArgumentOutOfRangeException(nameof(rule), rule, "Invalid bag generation rule");
            }
            return nextPieceType;
        }

        private void shuffle<T>( IList<char> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                char value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
