using System;

namespace helloword
{
    public abstract class Component
    {
       public abstract void Operation();
    }

    public class ConcreteComponent : Component
    {
        public override void Operation() => Console.WriteLine("具体对象的请求");
    }

    public abstract class Decorator : Component
    {
        protected Component component;
        public void setComponent(Component component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }


    class ConcreteDecoratorA:Decorator
    {
        private string addedstate;
        public override void Operation()
        {
            base.Operation();
            addedstate="new state";
            Console.WriteLine("具体装饰对象A的操作");
        }
    }
    class ConcreteDecoratorB:Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("具体装饰对象B的操作");
        }
    }
}


    ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();
            d1.setComponent(c);
            d2.setComponent(d1);
            d2.Operation();
            Console.ReadLine();


            // 具体对象的请求
            // 具体装饰对象A的操作
            // 具体装饰对象B的操作