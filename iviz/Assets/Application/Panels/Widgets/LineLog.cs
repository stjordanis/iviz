﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

namespace Iviz.App
{
    public class LineLog : MonoBehaviour, IWidget
    {
        [SerializeField] Text text = null;
        [SerializeField] GameObject content = null;
        [SerializeField] Scrollbar vertical = null;

        readonly List<string> lines = new List<string>();
        const int maxLines = 100;

        bool active = true;
        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (value)
                {
                    vertical.value = 0;
                    Flush();
                }
            }
        }

        public void Add(string text)
        {
            string[] sublines = text.Split('\n');
            lines.AddRange(sublines);
        }

        public void Flush()
        {
            if (!Active)
            {
                return;
            }

            float value = vertical.value;

            int overflow = lines.Count - maxLines;
            if (overflow > 0)
            {
                lines.RemoveRange(0, overflow);
            }

            StringBuilder str = new StringBuilder();
            lines.ForEach(x => str.AppendLine(x));

            text.text = str.ToString();
            float y = text.preferredHeight;

            RectTransform ctransform = (RectTransform)content.transform;
            ctransform.sizeDelta = new Vector2(0, y);

            if (value < 0.05f)
            {
                vertical.value = 0;
            }
        }

        public void ClearSubscribers()
        {
            Active = false;
        }
    }
}