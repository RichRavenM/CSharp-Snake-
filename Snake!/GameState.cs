using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_
{
    public class GameState
    {
        public int Rows { get; }
        public int Cols { get; }

        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }


        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;

            AddSnake();
            AddFood();
        }

        private void AddSnake ()
        {
            int r = Rows / 2;

            for (int i = 1; i <= 3; i++)
            {
                Grid[r, i] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, i));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols;j++)
                {
                    if (Grid[i,j] == GridValue.Empty)
                    {
                        yield return new Position(i, j);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0)
                return;

            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;
        }

        public Position HeadPosition()
        {
            return snakePositions.First.Value;

        }

        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveHead()
        {
            Position tail = TailPosition();
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        public void ChangeDirection(Direction direction)
        {
            Dir = direction;
        }



    }
}
