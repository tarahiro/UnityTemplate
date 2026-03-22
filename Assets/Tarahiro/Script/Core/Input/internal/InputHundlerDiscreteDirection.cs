using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class InputHundlerDiscreteDirection : IInputHundlerDiscreteDirection
    {
        [Inject] PureSingletonKey _key;
        [Inject] PureSingletonInput _input;

        Dictionary<Direction, float> _lastPressedTime = new Dictionary<Direction, float>();

        float c_time = .3f;

        public Vector2Int InputtedDiscreteDirection()
        {
            Vector2Int _returnable = Vector2Int.zero;


            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (_key.IsKeyDown(DirectionToKeyCode(direction)))
                {
                    _returnable += DirectionToVector(direction);
                }
                else
                {
                    if (_key.IsKey(DirectionToKeyCode(direction)))
                    {
                        if (_lastPressedTime.ContainsKey(direction))
                        {
                            if (Time.time - _lastPressedTime[direction] > c_time)
                            {
                                _returnable += DirectionToVector(direction);
                            }
                        }
                        else if (_key.TryGetPressingTimeSince(DirectionToKeyCode(direction), out float sinceTime))
                        {
                            if (Time.time - sinceTime > c_time)
                            {
                                _returnable += DirectionToVector(direction);
                            }

                        }

                    }
                }
            }


            return _returnable;
        }

        public void NotifyUse(Vector2Int direction)
        {
            Direction dir = VectorToDirection(direction);
            if (_lastPressedTime.ContainsKey(dir))
            {
                _lastPressedTime[dir] = Time.time;
            }
            else
            {
                _lastPressedTime.Add(dir, Time.time);
            }
            _input.AvailableInputted();
        }

        enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }

        KeyCode DirectionToKeyCode(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return KeyCode.RightArrow;

                case Direction.Left:
                    return KeyCode.LeftArrow;

                case Direction.Up:
                    return KeyCode.UpArrow;

                case Direction.Down:
                    return KeyCode.DownArrow;

                default:
                    Log.DebugLog("不正なdirectionです：" + direction);
                    return KeyCode.None;
            }
        }

        Vector2Int DirectionToVector(Direction direction)
        {

            switch (direction)
            {
                case Direction.Right:
                    return Vector2Int.right;

                case Direction.Left:
                    return Vector2Int.left;

                case Direction.Up:
                    return Vector2Int.up;

                case Direction.Down:
                    return Vector2Int.down;

                default:
                    Log.DebugLog("不正なdirectionです：" + direction);
                    return Vector2Int.zero;
            }
        }

        Direction VectorToDirection(Vector2Int vec)
        {
            if(vec == Vector2Int.right)
            {
                return Direction.Right;
            }else if (vec == Vector2Int.left)
            {
                return Direction.Left;
            }
            else if(vec == Vector2Int.up)
            {
                return Direction.Up;
            }
            else if(vec == Vector2Int.down)
            {
                return Direction.Down;
            }
            else
            {
                Log.DebugAssert("不正なベクトルです:" + vec);
                return Direction.Right;
            }
        }
    }
}