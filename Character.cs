using System;
using System.Drawing;
using System.Reflection;

namespace michaelsparty
{
    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
    public class Character : ICharacter
    {
        public (int x, int y) Position { get; set; }
        private Direction currentDirection;
        private Image characterImage;
        private int gridWidth;
        public bool IsAtEdge { get; set; }  
        public Character((int x, int y) startPosition)
        {
            Position = startPosition;
            currentDirection = Direction.Right; 
            LoadCharacterImage();
        }

        private void LoadCharacterImage()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "michaelsparty.Resources.lilrat.png";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        characterImage = Image.FromStream(stream);
                    }
                    else
                    {
                        Console.WriteLine("Image resource not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image: " + ex.Message);
            }
        }
        public void ResetPosition()
        {
            Position = (0, 0); 
            currentDirection = Direction.Right;
            IsAtEdge = false; 

        }

        public Image RotateCharacterImage(int width, int height)
        {
            int rotationAngle = currentDirection switch
            {
                Direction.Right => 0,
                Direction.Down => 90,
                Direction.Left => 180,
                Direction.Up => 270,
                _ => 0, 
            };

            Bitmap resizedImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.TranslateTransform(width / 2, height / 2);
                g.RotateTransform(rotationAngle);
                g.TranslateTransform(-width / 2, -height / 2);
                
                g.DrawImage(characterImage, 0, 0, width, height);
            }

            return resizedImage;
        }

        public void Move()
        {
            int minBound = -7;
            int maxBound = 7;

            var newPosition = Position;
            switch (currentDirection)
            {
                case Direction.Right:
                    newPosition = (Position.x + 1, Position.y);
                    break;
                case Direction.Down:
                    newPosition = (Position.x, Position.y - 1);
                    break;
                case Direction.Left:
                    newPosition = (Position.x - 1, Position.y);
                    break;
                case Direction.Up:
                    newPosition = (Position.x, Position.y + 1);
                    break;
            }

            if (newPosition.x < minBound || newPosition.x > maxBound || newPosition.y < minBound || newPosition.y > maxBound)
            {
                IsAtEdge = true;
            }
            else
            {
                IsAtEdge = false;
                Position = newPosition; 
            }
        }




        public void Turn(string direction)
        {
            if (direction == "right")
            {
                currentDirection = (Direction)(((int)currentDirection + 1) % Enum.GetValues(typeof(Direction)).Length);
            }
            else if (direction == "left")
            {
                currentDirection = (Direction)(((int)currentDirection - 1 + Enum.GetValues(typeof(Direction)).Length) % Enum.GetValues(typeof(Direction)).Length);
            }
        }


    }
}
