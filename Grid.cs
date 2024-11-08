using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace michaelsparty
{
    public partial class Grid : UserControl
    {
        private int gridSize;
        public Label[,] gridLabels;
        private HashSet<(int x, int y)> visitedPositions = new HashSet<(int x, int y)>();
        public ICharacter Character { get; set; }

        public Grid(int size, ICharacter character)
        {
            gridSize = size;
            InitializeComponent();
            Character = character; 
            InitializeGrid();
            UpdateGridDisplay();
        }

        private void InitializeGrid()
        {
            Panel gridPanel = new Panel
            {
                Size = new Size(700, 700),
            };
            this.Controls.Add(gridPanel);

            gridLabels = new Label[gridSize, gridSize];
            int cellSize = gridPanel.Width / gridSize;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Label cell = new Label
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(cellSize, cellSize),
                        Location = new Point(i * cellSize, j * cellSize),
                        BackColor = Color.White
                    };
                    gridLabels[i, j] = cell;
                    gridPanel.Controls.Add(cell);
                }
            }
        }

        private void UpdateGridDisplay()
        {
            foreach (var label in gridLabels)
            {
                label.BackColor = Color.White;
                label.Image = null; 
            }

            foreach (var position in visitedPositions)
            {
                int displayX = gridSize / 2 + position.x;
                int displayY = gridSize / 2 - position.y;

                if (displayX >= 0 && displayX < gridSize && displayY >= 0 && displayY < gridSize)
                {
                    gridLabels[displayX, displayY].BackColor = Color.LightBlue;
                }
            }

            int centre = gridSize / 2;
            int currentDisplayX = centre + Character.Position.x;
            int currentDisplayY = centre - Character.Position.y;

            if (currentDisplayX >= 0 && currentDisplayX < gridSize && currentDisplayY >= 0 && currentDisplayY < gridSize)
            {
                int cellSize = gridLabels[0, 0].Width;
                gridLabels[currentDisplayX, currentDisplayY].Image = Character.RotateCharacterImage(cellSize, cellSize);
            }
        }

        public async void ExecuteCommands(List<Commands> commands)
        {
            foreach (var command in commands)
            {
                if (command is Move moveCommand)
                {
                    for (int i = 0; i < moveCommand.Steps; i++)
                    {
                        MoveCharacter();
                        UpdateGridDisplay();
                        await Task.Delay(400);
                    }
                }
                else if (command is Turn turnCommand)
                {
                    TurnCharacter(turnCommand.Direction);
                }
                else if (command is RepeatUntilEdgeCommand repeatUntilEdgeCommand)
                {
                    while (!Character.IsAtEdge)
                    {
                        foreach (var innerCommand in repeatUntilEdgeCommand.Commands)
                        {
                            if (innerCommand is Move move)
                            {
                                for (int i = 0; i < move.Steps; i++)
                                {
                                    MoveCharacter();
                                    UpdateGridDisplay();
                                    await Task.Delay(400); 
                                }
                            }
                            else if (innerCommand is Turn turn)
                            {
                                TurnCharacter(turn.Direction);
                            }
                        }

                        Character.IsAtEdge = CheckIfAtEdge();  
                    }
                }
            }
        }


        private bool CheckIfAtEdge()
        {
            int maxBound = 7;
            return Character.Position.x >= maxBound || Character.Position.x <= -maxBound || Character.Position.y >= maxBound || Character.Position.y <= -maxBound;
        }



        public void ResetGrid()
        {
            visitedPositions.Clear();

            Character.ResetPosition();

            UpdateGridDisplay();
        }

        private void MoveCharacter()
        {
            visitedPositions.Add(Character.Position);
            Character.Move();
        }

        private void TurnCharacter(string direction)
        {
            Character.Turn(direction);
        }

        public void LoadAndDisplayGrid(List<List<char>> gridData)
        {
            if (gridData == null || gridData.Count != gridSize || gridData[0].Count != gridSize)
            {
                MessageBox.Show("File grid size does not match the current grid size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    char cellChar = gridData[y][x];

                    if (cellChar == '+')
                    {
                        gridLabels[x, y].BackColor = Color.Gray;
                    }
                    else if (cellChar == 'o')
                    {
                        gridLabels[x, y].BackColor = Color.White; 
                    }
                    else if (cellChar == 'x')
                    {
                        gridLabels[x, y].BackColor = Color.Red; 
                    }
                }
            }

            Character.Position = (-7, 7);
        }

    }
}
