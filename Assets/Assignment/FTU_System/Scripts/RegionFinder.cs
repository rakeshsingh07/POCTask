using System.Collections.Generic;
using UnityEngine;

namespace JG.FTUSystem
{
    /// <summary>
    /// Get area covered by the image 
    /// Check if any part of the area is overlapping with the card
    /// Get covered area
    /// </summary>
    public class RegionFinder
    {

        //Get covered area 
        public static List<Rectangle> GetCoverRects (RectTransform canvas, List<RectTransform> rectsToExclude)
        {
            List<Rectangle> convertedItems = new List<Rectangle>();
            Rectangle bigRectangle = GetRectFromPosAndRectTransform(canvas, Vector2.zero);
            for (int i = 0; i < rectsToExclude.Count; i++)
            {
                convertedItems.Add(ChangeRectTransformToRectangle(canvas, rectsToExclude[i]));
            }

            List<float> xPoints = GetSortedXPoints(convertedItems, bigRectangle);
            List<float> yPoints = GetSortedYPoints(convertedItems, bigRectangle);

            List<Rectangle> overlayRects = new List<Rectangle>();
            float xMin, xMax, yMin, yMax = 0;
            for (int i = 1; i < yPoints.Count; i++)
            {
                for (int j = 1; j < xPoints.Count; j++)
                {
                    xMin = xPoints[j - 1];

                    xMax = xPoints[j];

                    yMin = yPoints[i - 1];

                    yMax = yPoints[i];


                    Rectangle rectangle = GetRectangle(xMin, xMax, yMin, yMax);
                    if (!IsOverlapping(rectangle, convertedItems))
                    {
                        overlayRects.Add(rectangle);
                    }
                }
            }
            CombineInXdirection(overlayRects);
            return overlayRects;
        }


        //Combine rect area 
        //Store the rect details
        private static void CombineInXdirection(List<Rectangle> rectangles)
        {
            for (int i = rectangles.Count - 1; i > 0; i--)
            {
                if (rectangles[i].v00.x == rectangles[i - 1].v11.x && rectangles[i].v00.y == rectangles[i - 1].v00.y
                && rectangles[i].v11.y == rectangles[i - 1].v11.y)
                {
                    Rectangle rectangle = GetRectangle(rectangles[i - 1].v00.x, rectangles[i].v11.x,
                    rectangles[i - 1].v00.y, rectangles[i].v11.y);
                    rectangles.Insert(i - 1, rectangle);
                    rectangles.RemoveAt(i);
                    rectangles.RemoveAt(i);
                }
            }
        }

        //Get the rectangle points
        private static Rectangle GetRectangle(float xMin, float xMax, float yMin, float yMax)
        {
            Rectangle rectangle = new Rectangle
            {
                v00 = new Vector2(xMin, yMin),
                v10 = new Vector2(xMax, yMin),
                v01 = new Vector2(xMin, yMax),
                v11 = new Vector2(xMax, yMax)
            };
            return rectangle;
        }

        //Check if any part is overlapping here
        private static bool IsOverlapping(Rectangle subRectangle, List<Rectangle> rectangles)
        {
            for (int i = 0; i < rectangles.Count; i++)
            {
                if (subRectangle.v00.x >= rectangles[i].v00.x && subRectangle.v11.x <= rectangles[i].v11.x && 
                    subRectangle.v00.y >= rectangles[i].v00.y && subRectangle.v11.y <= rectangles[i].v11.y)
                    return true;
            }
            return false;
        }


        //Draw ractangel 
        private static Rectangle ChangeRectTransformToRectangle(RectTransform canvas, RectTransform rectTransform)
        {
            Vector2 pos = canvas.InverseTransformPoint(rectTransform.position);
            return GetRectFromPosAndRectTransform(rectTransform, pos);
        }

        //Get rect transform amd postion 
        private static Rectangle GetRectFromPosAndRectTransform(RectTransform rectTransform, Vector2 pos)
        {
            var rect = rectTransform.rect;
            Rectangle rectangle = new Rectangle
            {
                v00 = new Vector2(pos.x - rect.width / 2, pos.y - rect.height / 2),
                v10 = new Vector2(pos.x + rect.width / 2, pos.y - rect.height / 2),
                v01 = new Vector2(pos.x - rect.width / 2, pos.y + rect.height / 2),
                v11 = new Vector2(pos.x + rect.width / 2, pos.y + rect.height / 2)
            };
            return rectangle;
        }


        //Get sorted x points 
        private static List<float> GetSortedXPoints(List<Rectangle> rectangles, Rectangle bigRect)
        {
            List<float> list = new List<float>();
            AddToList(list, bigRect.v00.x);
            for (int i = 0; i < rectangles.Count; i++)
            {
                AddToList(list, rectangles[i].v00.x);
                AddToList(list, rectangles[i].v11.x);
            }
            AddToList(list, bigRect.v11.x);
            list.Sort((x, y) =>
            {
                if (x < y) return -1;
                return 1;
            });
            return list;
        }

        //Get sorted y points
        private static List<float> GetSortedYPoints(List<Rectangle> rectangles, Rectangle bigRect)
        {
            List<float> list = new List<float>();
            AddToList(list, bigRect.v00.y);
            for (int i = 0; i < rectangles.Count; i++)
            {
                AddToList(list, rectangles[i].v00.y);
                AddToList(list, rectangles[i].v11.y);
            }
            AddToList(list, bigRect.v11.y);
            list.Sort((x, y) =>
            {
                if (x < y) return -1;
                return 1;
            });
            return list;
        }

        //Add all the points in the list
        private static void AddToList(List<float> list, float x)
        {
            if (!list.Contains(x)) list.Add(x);
        }
    }
}
