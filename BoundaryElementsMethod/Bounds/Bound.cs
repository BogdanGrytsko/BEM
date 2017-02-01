using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEM.BoundaryElements;
using BEM.Common.Points;
using BEM.Enum;

namespace BEM.Bounds
{
    public abstract class Bound<T>
        where T : IPoint
    {
        public List<BoundaryElement<T>> Elements { get; private set; }

        protected Bound()
        {
            Elements = new List<BoundaryElement<T>>();
        }

        public void Add(params Bound<T>[] bounds)
        {
            foreach (var element in bounds.SelectMany(bound => bound.Elements))
            {
                element.Bound = this;
                Elements.Add(element);
            }
        }

        public void Add(IEnumerable<BoundaryElement<T>> elements)
        {
            foreach (var elem in elements)
            {
                elem.Bound = this;
                Elements.Add(elem);
            }
        }

        public abstract bool Inside(T x);

        public abstract T BottomLeftCorner { get; }

        public abstract T TopRightCorner { get; }

        public virtual IEnumerable<T> ObservablePoints
        {
            get { return Elements.Select(el => el.Center); }
        }

        public bool IsOuter { get; set; }

        public int Count
        {
            get
            {
                return Elements.Count;
            }
        }

        public BoundNumber Name { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Elements)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}