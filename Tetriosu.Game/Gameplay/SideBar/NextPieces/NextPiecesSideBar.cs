using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osuTK.Graphics;
using Tetriosu.Game.Gameplay.Components;
using Tetriosu.Game.Gameplay.Components.BagGenerator;

namespace Tetriosu.Game.Gameplay.SideBar.NextPieces
{
    public partial class NextPiecesSideBar : CompositeDrawable
    {
        private Container nextPiecesContainer;
        private int nextPiecesCount
        {
            get;
            set
            {
                if (value > 5 || value <= 0)
                {
                    field = 5;
                    return;
                }

                field = value;
            }
        }

        public NextPiecesSideBar(int numberOfPiecesToShow)
        {
            nextPiecesCount = numberOfPiecesToShow;
            AutoSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new Box()
                {
                    Colour = Color4.Black,
                    Width = GameSetupValues.BLOCK_SIZE * 6f,
                    Height = GameSetupValues.BLOCK_SIZE * (nextPiecesCount * 3 + 1),
                },
                nextPiecesContainer = new Container()
                {
                    Width = GameSetupValues.BLOCK_SIZE * 6f,
                    Height = GameSetupValues.BLOCK_SIZE * (nextPiecesCount * 3 + 1),
                },
            };
        }

        public void LoadNextPiecesSideBar()
        {
            nextPiecesContainer.Clear(true);

            int i = 0;
            foreach (string pType in BagGenerator.NextPieceTypes)
            {
                if (i >= nextPiecesCount) { break; }

                nextPiecesContainer.Add(new SideBarFullPiece(pType)
                {
                    Y = GameSetupValues.BLOCK_SIZE * (i * 3 + 1) 
                });

                i++;
            }
        }

        public void UpdateNextPiecesSideBar()
        {
            nextPiecesContainer.Remove(nextPiecesContainer.Children[0], true);

            foreach (Drawable? child in nextPiecesContainer.Children)
            {
                child.Y -= GameSetupValues.BLOCK_SIZE * 3;
            }

            nextPiecesContainer.Add(new SideBarFullPiece(BagGenerator.NextPieceTypes[nextPiecesCount - 1])
            {
                Y = GameSetupValues.BLOCK_SIZE * (4 * 3 + 1)
            });
        }
    }
}
