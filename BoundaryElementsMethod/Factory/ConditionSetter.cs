using System.Collections.Generic;

using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Enum;

namespace BEM.Factory
{
    public class ConditionSetter
    {
        public static List<BoundWithCondition<Point2D>> SetDirichletCondition(
            params Bound<Point2D>[] bounds)
        {
            var list = new List<BoundWithCondition<Point2D>>();
            for (int i = 0; i < bounds.Length - 1; i++)
            {
                list.Add(new BoundWithCondition<Point2D>(bounds[i], ConditionType.Dirichlet, FunctionFactory.F));
            }
            list.Add(new BoundWithCondition<Point2D>(bounds[bounds.Length - 1], ConditionType.Dirichlet,
                FunctionFactory.G));
            return list;
        }

        public static List<BoundWithCondition<Point3D>> SetDirichletCondition(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Dirichlet, FunctionFactory.Gother)
            };
        }
        public static List<BoundWithCondition<Point3D>> SetDirichletConditionSphere(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Dirichlet, FunctionFactory.GotherSphere)
            };
        }
        public static List<BoundWithCondition<Point3D>> SetNeumannCondition(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Neumann, FunctionFactory.G)
            };
        }

        public static List<BoundWithCondition<Point3D>> SetRobinCondition(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Robin, FunctionFactory.G)
            };
        }

        public static List<BoundWithCondition<Point2D>> SetNeumannCondition(Bound<Point2D> bound)
        {
            return new List<BoundWithCondition<Point2D>>
            {
                new BoundWithCondition<Point2D>(bound, ConditionType.Neumann, FunctionFactory.G)
            };
        }


        public static List<BoundWithCondition<Point3D>> SetKirghoffCondition(Parallelepiped bound)
        {
            var leftRight = new Pane();
            leftRight.Add(bound.LeftPane);
            leftRight.Add(bound.RightPane);
            var rest = new Pane();
            rest.Add(bound.FrontPane);
            rest.Add(bound.BackPane);
            rest.Add(bound.TopPane);
            rest.Add(bound.BottomPane);
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(leftRight, ConditionType.Robin, FunctionFactory.Gother),
                new BoundWithCondition<Point3D>(rest, ConditionType.Robin, FunctionFactory.G),
                ////   new BoundWithCondition<Point3D>(topbottom, ConditionType.Neumann, FunctionFactory.Gother)
            };
        }

        public static List<BoundWithCondition<Point3D>> SetMixedCondition(Parallelepiped bound)
        {
            var leftRight = new Pane();
            leftRight.Add(bound.LeftPane);
            leftRight.Add(bound.RightPane);
            var rest = new Pane();
            rest.Add(bound.FrontPane);
            rest.Add(bound.BackPane);
            rest.Add(bound.TopPane);
            rest.Add(bound.BottomPane);
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(leftRight, ConditionType.Dirichlet, FunctionFactory.G),
                new BoundWithCondition<Point3D>(rest, ConditionType.Robin, FunctionFactory.G),
                // new BoundWithCondition<Point3D>(topbottom, ConditionType.Neumann, FunctionFactory.G)
            };
        }

        public static IEnumerable<BoundWithCondition<Point3D>> SetBoundSphere(params Bound<Point3D>[] bounds)
        {
            foreach (var sphere in bounds)
            {
                yield return new BoundWithCondition<Point3D>(sphere, ConditionType.Dirichlet, null);
            }
        }
        public static IEnumerable<BoundWithCondition<Point3D>> SetBoundSphereKirghoffCondition(params Bound<Point3D>[] bounds)
        {
            foreach (var sphere in bounds)
            {
                yield return new BoundWithCondition<Point3D>(sphere, ConditionType.Robin, FunctionFactory.GSphere);
            }
        }


    }

    public class CopyOfConditionSetter
    {
        public static List<BoundWithCondition<Point2D>> SetDirichletCondition(
            params Bound<Point2D>[] bounds)
        {
            var list = new List<BoundWithCondition<Point2D>>();
            for (int i = 0; i < bounds.Length - 1; i++)
            {
                list.Add(new BoundWithCondition<Point2D>(bounds[i], ConditionType.Dirichlet, FunctionFactory.F));
            }
            list.Add(new BoundWithCondition<Point2D>(bounds[bounds.Length - 1], ConditionType.Dirichlet,
                FunctionFactory.G));
            return list;
        }

        public static List<BoundWithCondition<Point3D>> SetDirichletCondition(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Dirichlet, FunctionFactory.Gother)
            };
        }
        public static List<BoundWithCondition<Point3D>> SetDirichletConditionSphere(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Dirichlet, FunctionFactory.GotherSphere)
            };
        }
        public static List<BoundWithCondition<Point3D>> SetNeumannCondition(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Neumann, FunctionFactory.G)
            };
        }

        public static List<BoundWithCondition<Point3D>> SetRobinCondition(Bound<Point3D> bound)
        {
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(bound, ConditionType.Robin, FunctionFactory.G)
            };
        }

        public static List<BoundWithCondition<Point2D>> SetNeumannCondition(Bound<Point2D> bound)
        {
            return new List<BoundWithCondition<Point2D>>
            {
                new BoundWithCondition<Point2D>(bound, ConditionType.Neumann, FunctionFactory.G)
            };
        }


        public static List<BoundWithCondition<Point3D>> SetKirghoffCondition(Parallelepiped bound)
        {
            var leftRight = new Pane();
            leftRight.Add(bound.LeftPane);
            leftRight.Add(bound.RightPane);
            var rest = new Pane();
            rest.Add(bound.FrontPane);
            rest.Add(bound.BackPane);
            rest.Add(bound.TopPane);
            rest.Add(bound.BottomPane);
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(leftRight, ConditionType.Robin, FunctionFactory.Gother),
                new BoundWithCondition<Point3D>(rest, ConditionType.Robin, FunctionFactory.G),
                ////   new BoundWithCondition<Point3D>(topbottom, ConditionType.Neumann, FunctionFactory.Gother)
            };
        }

        public static List<BoundWithCondition<Point3D>> SetMixedCondition(Parallelepiped bound)
        {
            var leftRight = new Pane();
            leftRight.Add(bound.LeftPane);
            leftRight.Add(bound.RightPane);
            var rest = new Pane();
            rest.Add(bound.FrontPane);
            rest.Add(bound.BackPane);
            rest.Add(bound.TopPane);
            rest.Add(bound.BottomPane);
            return new List<BoundWithCondition<Point3D>>
            {
                new BoundWithCondition<Point3D>(leftRight, ConditionType.Dirichlet, FunctionFactory.G),
                new BoundWithCondition<Point3D>(rest, ConditionType.Robin, FunctionFactory.G),
                // new BoundWithCondition<Point3D>(topbottom, ConditionType.Neumann, FunctionFactory.G)
            };
        }

        public static IEnumerable<BoundWithCondition<Point3D>> SetBoundSphere(params Bound<Point3D>[] bounds)
        {
            foreach (var sphere in bounds)
            {
                yield return new BoundWithCondition<Point3D>(sphere, ConditionType.Dirichlet, null);
            }
        }
        public static IEnumerable<BoundWithCondition<Point3D>> SetBoundSphereKirghoffCondition(params Bound<Point3D>[] bounds)
        {
            foreach (var sphere in bounds)
            {
                yield return new BoundWithCondition<Point3D>(sphere, ConditionType.Robin, FunctionFactory.GSphere);
            }
        }


    }
}
