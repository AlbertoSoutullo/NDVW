using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Position
{
    public uint Y;
    public uint X;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public class MapGeneratorMaze
{
    private uint size;
    private int randomSeed;
    private Position initialPosition;
    
    public MapGeneratorMaze(
        uint size,
        int randomSeed,
        uint initialPositionX,
        uint initialPositionY
    )
    {
        this.size = size;
        this.randomSeed = randomSeed;
        this.initialPosition = new Position { Y = initialPositionY, X = initialPositionX };
    }
    
    public WallState[,] Generate()
    {
        WallState[,] maze = new WallState[this.size, this.size];
        maze = this.ApplyInitialState(maze);
        maze = this.ApplyRecursiveBacktracker(maze);
        return maze;
    }

    private WallState[,] ApplyInitialState(WallState[,] maze)
    {
        WallState initialState = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                maze[i, j] = initialState;
            }
        }
        return maze;
    }

    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    private WallState[,] ApplyRecursiveBacktracker(WallState[,] maze)
    {
        var rng = new System.Random(this.randomSeed);
        var positionStack = new Stack<Position>();

        maze[this.initialPosition.Y, this.initialPosition.X] |= WallState.VISITED;
        positionStack.Push(initialPosition);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = this.GetUnvisitedNeighbours(current, maze);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.Y, current.X] &= ~randomNeighbour.SharedWall;
                maze[nPosition.Y, nPosition.X] &= ~GetOppositeWall(randomNeighbour.SharedWall);
                maze[nPosition.Y, nPosition.X] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    private List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze)
    {
        var list = new List<Neighbour>();

        if (p.Y < this.size - 1) // down
        {
            if (!maze[p.Y + 1, p.X].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        Y = p.Y + 1,
                        X = p.X
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }

        if (p.Y > 0) // up
        {
            if (!maze[p.Y - 1, p.X].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        Y = p.Y - 1,
                        X = p.X
                    },
                    SharedWall = WallState.UP
                });
            }
        }
        
        if (p.X < this.size - 1) // to the right
        {
            if (!maze[p.Y, p.X + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        Y = p.Y,
                        X = p.X + 1
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }
        
        if (p.X > 0) // to the left
        {
            if (!maze[p.Y, p.X - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        Y = p.Y,
                        X = p.X - 1
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }

        return list;
    }
}