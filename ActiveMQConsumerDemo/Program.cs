using Apache.NMS;
using System;

namespace ActiveMQConsumerDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("ActiveMQ Consumer Demo");

            string topic = "TextQueue";

            string brokerUri = $"activemq:tcp://192.168.250.198:61616";  // Default port
            NMSConnectionFactory factory = new NMSConnectionFactory(brokerUri);

            using (IConnection connection = factory.CreateConnection("artemis", "simetraehcapa"))
            {
                connection.Start();
                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetQueue(topic))
                using (IMessageConsumer consumer = session.CreateConsumer(dest))
                {
                    IMessage msg = consumer.Receive();
                    if (msg is ITextMessage)
                    {
                        ITextMessage txtMsg = msg as ITextMessage;
                        string body = txtMsg.Text;

                        Console.WriteLine($"Received message: {txtMsg.Text}");
                    }
                    else
                    {
                        Console.WriteLine("Unexpected message type: " + msg.GetType().Name);
                    }
                }
            }

            Console.WriteLine($"");
            Console.WriteLine($"Press any key to finish.");
            Console.ReadKey(true);
        }
    }
}