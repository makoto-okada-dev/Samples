using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPReciever
{
    class Program
    {
        static void Main(string[] args)
        {
            //バインドするローカルIPとポート番号
            var localIpString = "127.0.0.1";
            var localAddress = IPAddress.Parse(localIpString);
            int localPort = 2002;

            //UdpClientを作成し、ローカルエンドポイントにバインドする
            var localEP = new IPEndPoint(localAddress, localPort);
            using (var udp = new UdpClient(localPort))
            {

                for (; ; )
                {
                    //データを受信する
                    IPEndPoint remoteEP = null;
                    byte[] rcvBytes = udp.Receive(ref remoteEP);

                    //データを文字列に変換する
                    string rcvMsg = Encoding.UTF8.GetString(rcvBytes);

                    // 受信データ表示
                    //Console.WriteLine("Addr:{0} / Port:{1} / {2}", remoteEP.Address, remoteEP.Port, rcvMsg);
                    Console.WriteLine(rcvMsg);

                    //"exit"を受信したら終了
                    if (rcvMsg.Equals("exit"))
                    {
                        break;
                    }
                }
            }
        }
    }
}
