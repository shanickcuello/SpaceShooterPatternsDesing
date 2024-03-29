﻿using System.Collections.Generic;
using UnityEngine;

namespace Features.UI
{
    public class ScreenManager : MonoBehaviour
    {
        Stack<IScreen> _stack;

        public string lastResult;

        static public ScreenManager instance;

        private void Awake()
        {
            _stack = new Stack<IScreen>();

            instance = this;
        }

        public void Pop()
        {
            if (_stack.Count <= 1)
                return;

            lastResult = _stack.Pop().Free();

            if (_stack.Count > 0)
                _stack.Peek().Activate();
        }

        public void Push(IScreen screen)
        {
            if (_stack.Count > 0)
                _stack.Peek().Deactivate();

            _stack.Push(screen);
            screen.Activate();
        }

        public void Push(string resource)
        {
            var go = Instantiate(Resources.Load<GameObject>(resource));
            Push(go.GetComponent<IScreen>());
        }
    }
}
