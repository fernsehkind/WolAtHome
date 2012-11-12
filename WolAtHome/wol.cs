using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace WolAtHome
{
    class WOL : UdpClient
    {
        private string _mac;
        public string Mac
        {
            get { return _mac; }
            set { _mac = value; }
        }

        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public WOL(string mac) 
            : this(mac, "255.255.255.255", 9)
        {
        }

        public WOL(string mac, string ip)
            : this(mac, ip, 9)
        {
        }


        public WOL(string mac, string ip, int port) : base()
        {
            _mac = mac;
            _ip = ip;
            _port = port;
        }

        public void WakeUp()
        {
            this.Connect(IPAddress.Parse(_ip), _port);

            if (string.Compare(_ip, "255.255.255.255") == 0)
            {
                this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 0);
            }

            byte[] sendBytes = new byte[102];

            for (int index = 0; index < 6; index++)
            {
                sendBytes[index] = 0xFF;
            }

            byte[] macAddressInByte = new byte[6];
            for (int index = 0; index < 6; index++)
            {
                macAddressInByte[index] = byte.Parse(_mac.Substring(index*2,2), NumberStyles.HexNumber);
            }
            for (int index = 6; index < sendBytes.Length; index++)
            {
                sendBytes[index] = macAddressInByte[index % 6];
            }

            this.Send(sendBytes, sendBytes.Length);
        }
    }
}