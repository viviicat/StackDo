﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackDo.Interface;

namespace StackDo.Display
{
    class DetailedTodoContainerDisplay : ITodoContainerDisplay
    {
        private ITodoDisplay _todoDisplay;
        private ITodoDisplay _parentDisplay;
        private ITodoDisplay _childDisplay;

        public DetailedTodoContainerDisplay(ITodoDisplay todoDisplay, ITodoDisplay parentDisplay, ITodoDisplay childDisplay)
        {
            _todoDisplay = todoDisplay;
            _parentDisplay = parentDisplay;
            _childDisplay = childDisplay;
        }

        public string Display(ITodoContainer container)
        {
            StringBuilder sb = new StringBuilder();

            if (_parentDisplay != null)
            {
                if (container.Parent != null)
                {
                    ITodoContainer parent = container.Parent;
                    while (parent != null)
                    {
                        if (parent.Todo != null)
                        {
                            sb.Insert(0, string.Format("{0} > ", _parentDisplay.Display(parent.Todo)));
                        }
                        else
                        {
                            sb.Insert(0, "root > ");
                        }

                        parent = parent.Parent;
                    }
                    sb.AppendLine("\n");
                }
                else
                {
                    sb.AppendLine("<root>");
                }
            }

            if (_todoDisplay != null && container.Todo != null)
            {
                sb.AppendLine(_todoDisplay.Display(container.Todo));
            }

            if (container.Children.Any())
            {
                sb.AppendFormat("{0} Children:\n", container.Children.Count());

                if (_childDisplay != null)
                {
                    int i = 0;
                    foreach (ITodo child in container.Children.Select(c => c.Todo))
                    {
                        sb.AppendFormat("  {0} {1}\n", i, _childDisplay.Display(child));
                        i++;
                    }
                }
            }
            else
            {
                sb.AppendLine("No TODOs.");
            }

            return sb.ToString();
        }
    }
}