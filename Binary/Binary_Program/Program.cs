using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace Binary_Program
{
    static class Program
    {

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        { 

            if (args.Length <= 0)
            {
                Console.WriteLine("引数を入力してください。\n読み込むファイルと書き出すファイルの間に半角スペースをおいてください");

                string var = Console.ReadLine();

                args = var.Split(' ');

            }

            //fileName = args[1];

            //File_Delete();
            for(int i = 0; i < args.Length; i++)
            {
                DateTime dateTime = DateTime.Now;

                string name = Path.GetFileName(args[i]);
                string fileName = "";

                fileName = ".\\"+name+"_" + i + "_" + dateTime.ToString("yy_MM_dd") + ".csv";

                Console.WriteLine(fileName);

                string text = "";

                string typeText = "";

                using (FileStream fs = File.Create(fileName)) ;

                try
                {
                    using (FileStream fs = new FileStream(args[i], FileMode.Open, FileAccess. Read,FileShare.None))
                    using (BinaryReader br = new BinaryReader(fs))
                    {

                        int h_Length;

                        h_Length = br.ReadInt32();

                        int h_items = br.ReadInt32();

                        byte[] dataType = new byte[h_items];

                        for (int j = 0; j < h_items; j++)
                        {
                            dataType[j] = br.ReadByte();

                            if (dataType[j] == 0)
                            {
                                typeText += "None";
                            }
                            else if (dataType[j] == 1)
                            {
                                typeText += "Int";
                            }
                            else if (dataType[j] == 2)
                            {
                                typeText += "Long";
                            }
                            else if (dataType[j] == 3)
                            {
                                typeText += "Float";
                            }
                            else if (dataType[j] == 4)
                            {
                                typeText += "Double";
                            }
                            else if (dataType[j] == 5)
                            {
                                typeText += "String";
                            }
                            else if (dataType[j] == 6)
                            {
                                typeText += "Bool";
                            }
                            else if (dataType[j] == 7)
                            {
                                typeText += "Byte";
                            }
                            else if (dataType[j] == 8)
                            {
                                typeText += "SByte";
                            }
                            else if (dataType[j] == 9)
                            {
                                typeText += "Short";
                            }
                            else if (dataType[j] == 10)
                            {
                                typeText += "Ushort";
                            }

                            int items = br.ReadByte();

                            var bytes = br.ReadBytes(items);

                            text += Encoding.UTF8.GetString(bytes);

                            if (j != h_items - 1)
                            {
                                text += ",";
                                typeText += ",";
                            }
                            
                        }

                        File_Write(text, fileName);

                        File_Write(typeText, fileName);

                        text = "";
                        typeText = "";

                        int d_Length = br.ReadInt32();

                        for (int k = 0; k < d_Length; k++)
                        {
                            for (int l = 0; l < h_items; l++)
                            {
                                if (dataType[l] == 1)
                                {
                                    int value = br.ReadByte();

                                    if (value <= 200)
                                    {

                                        text += value.ToString();
                                    }
                                    else
                                    {
                                        if (value == 201)
                                        {
                                            byte byt = br.ReadByte();

                                            text += byt.ToString();

                                        }
                                        else if (value == 202)
                                        {
                                            int it = br.ReadInt32();

                                            text += it.ToString();
                                        }
                                        else if (value == 203)
                                        {
                                            long lon = br.ReadInt64();

                                            text += lon.ToString();
                                        }
                                        else if (value == 204)
                                        {
                                            short sho = br.ReadInt16();

                                            text += sho.ToString();
                                        }
                                        else
                                        {
                                            text += "err";
                                        }
                                    }
                                }
                                else if (dataType[l] == 2)
                                {
                                    int value = br.ReadByte();

                                    if (value <= 200)
                                    {
                                        text += value.ToString();
                                    }
                                    else
                                    {
                                        if (value == 201)
                                        {
                                            byte byt = br.ReadByte();

                                            text += byt.ToString();
                                        }
                                        else if (value == 202)
                                        {
                                            int it = br.ReadInt32();

                                            text += it.ToString();
                                        }
                                        else if (value == 203)
                                        {
                                            long lon = br.ReadInt64();

                                            text += lon.ToString();
                                        }
                                        else if (value == 204)
                                        {
                                            short sho = br.ReadInt16();

                                            text += sho.ToString();
                                        }
                                        else
                                        {
                                            text += "err";
                                        }
                                    }
                                }
                                else if (dataType[l] == 3)
                                {
                                    float flo = br.ReadSingle();

                                    text += flo.ToString();
                                }
                                else if (dataType[l] == 4)
                                {
                                    double dob = br.ReadDouble();

                                    text += dob.ToString();
                                }
                                else if (dataType[l] == 5)
                                {
                                    int exist = br.ReadByte();

                                    if (exist == 1)
                                    {
                                        int items = br.ReadByte();

                                        var byts = br.ReadBytes(items);

                                        text += Encoding.UTF8.GetString(byts);
                                    }
                                    else if (exist == 0)
                                    {
                                        text += "null";
                                    }
                                }
                                else if (dataType[l] == 6)
                                {
                                    int it = br.ReadByte();

                                    text += it.ToString();
                                }
                                else if (dataType[l] == 7)
                                {
                                    byte byt = br.ReadByte();

                                    text += byt.ToString();
                                }
                                else if (dataType[l] == 8)
                                {
                                    sbyte sbyt = br.ReadSByte();

                                    text += sbyt.ToString();
                                }
                                else if (dataType[l] == 9)
                                {
                                    short sho = br.ReadInt16();

                                    text += sho.ToString();
                                }
                                else if (dataType[l] == 10)
                                {
                                    ushort usho = br.ReadUInt16();

                                    text += usho.ToString();
                                }
                                else if (dataType[l] == 0)
                                {
                                    text += "None";
                                }
                                else
                                {
                                    text += "err";
                                }

                                if(l != h_items-1)
                                {
                                    text += ",";
                                }
                                
                            }

                            Console.WriteLine(k + "個目");

                            File_Write(text, fileName);

                            text = "";
                        }

                        Console.WriteLine("書き出し完了");
                    }

                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message.ToString());

                    Console.WriteLine("\n");
                }
                finally
                {
                    
                }
            }
           
        }

        public static void File_Write(string str, string fileName)
        {
            Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");

            using (StreamWriter writer = new StreamWriter(fileName, true, encoding))
            {
                writer.WriteLine(str);
            }
        }

        //static void Data_Read(byte[] data)
        //{
        //    byte[] hedLength = new byte[4];
        //    int h_Length;

        //    for (int i = 0; i < 4; i++)
        //    {
        //        hedLength[i] = data[i];
        //    }


        //    h_Length = BitConverter.ToInt32(hedLength, 0);

        //    Header_Read(data,h_Length);


        //    byte[] datLength = new byte[4];

        //    int data_StartPosition = 4 + h_Length;

        //    for(int i = 0; i < 4; i++)
        //    {
        //        datLength[i] = data[i + data_StartPosition];

        //        //Console.Write("{0,2:X2} ", datLength[i]);
        //    }

        //    d_Length = BitConverter.ToInt32(datLength, 0);

        //    //Console.WriteLine(d_Length + "*"+head_Dat.Length);

        //    mainData = new string[d_Length, head_Dat.Length];

        //    int count = 0;
        //    int items = 0;
        //    int through = 0;
        //    int end = 0;

        //    int readMax = dataType.Length;

        //    int num = 0;

        //    bool ON = true;

        //    //Console.WriteLine(mainData[0, 0]);

        //    //Console.WriteLine(data.Length);

        //    for(int i = data_StartPosition + 4; i < data.Length; i++)
        //    {

        //        if (dataType[count] == 1)
        //        {
        //            if (ON)
        //            {
        //                if (data[i] <= 200)
        //                {
        //                    if (data[i] == 200)
        //                    {
        //                        mainData[num, count] = data[i].ToString();

        //                        //Console.WriteLine(num+" "+count+" "+mainData[num, count]);
        //                        count += 1;
        //                    }
        //                    else
        //                    {
        //                        mainData[num, count] = data[i].ToString();

        //                        //Console.WriteLine(num+" "+count+" "+mainData[num, count]);
        //                        count += 1;
        //                    }
        //                }
        //                else
        //                {
        //                    if (data[i] == 201)
        //                    {
        //                        byte byt = data[i + 1];

        //                        end = 1;
        //                        ON = false;
        //                    }
        //                    else if (data[i] == 202)
        //                    {
        //                        int it = BitConverter.ToInt32(data, i + 1);

        //                        mainData[num, count] = it.ToString();

        //                        //Console.WriteLine(num + " " + count + " " + mainData[num, count]);

        //                        end = 4;
        //                        ON = false;
        //                    }
        //                    else if (data[i] == 203)
        //                    {
        //                        long lon = BitConverter.ToInt64(data, i + 1);

        //                        mainData[num, count] = lon.ToString();

        //                        end = 8;
        //                        ON = false;
        //                    }
        //                    else if (data[i] == 204)
        //                    {
        //                        short sho = BitConverter.ToInt16(data, i + 1);

        //                        mainData[num, count] = sho.ToString();

        //                        end = 2;
        //                        ON = false;
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                if (through < end - 1)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }

        //        }
        //        else if (dataType[count] == 2)
        //        {
        //            if (ON)
        //            {
        //                if (data[i] <= 200)
        //                {
        //                    if (data[i] == 200)
        //                    {
        //                        mainData[num, count] = data[i].ToString();

        //                        //Console.WriteLine(num+" "+count+" "+mainData[num, count]);
        //                        count += 1;
        //                    }
        //                    else
        //                    {
        //                        mainData[num, count] = data[i].ToString();

        //                        //Console.WriteLine(num+" "+count+" "+mainData[num, count]);
        //                        count += 1;
        //                    }
        //                }
        //                else
        //                {
        //                    if (data[i] == 201)
        //                    {
        //                        byte byt = data[i + 1];

        //                        mainData[num, count] = byt.ToString();

        //                        end = 1;
        //                        ON = false;
        //                    }
        //                    else if (data[i] == 202)
        //                    {
        //                        int it = BitConverter.ToInt32(data, i + 1);

        //                        mainData[num, count] = it.ToString();

        //                        //Console.WriteLine(num + " " + count + " " + mainData[num, count]);

        //                        end = 4;
        //                        ON = false;
        //                    }
        //                    else if (data[i] == 203)
        //                    {
        //                        long lon = BitConverter.ToInt64(data, i + 1);

        //                        mainData[num, count] = lon.ToString();

        //                        end = 8;
        //                        ON = false;
        //                    }
        //                    else if (data[i] == 204)
        //                    {
        //                        short sho = BitConverter.ToInt16(data, i + 1);

        //                        mainData[num, count] = sho.ToString();

        //                        end = 2;
        //                        ON = false;
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                if (through < end - 1)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }
        //        }
        //        else if (dataType[count] == 3)
        //        {
        //            if (ON) 
        //            {
        //                float flo = BitConverter.ToSingle(data, i);

        //                mainData[num, count] = flo.ToString();

        //                end = 3;
        //                ON = false;
        //            }
        //            else
        //            {
        //                if (through < end - 1)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }

        //        }
        //        else if (dataType[count] == 4)
        //        {
        //            if (ON)
        //            {
        //                double dou = BitConverter.ToDouble(data, i);

        //                mainData[num, count] = dou.ToString();

        //                end = 7;
        //                ON = false;
        //            }
        //            else
        //            {
        //                if (through < end - 1)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }
        //        }
        //        else if (dataType[count] == 5)
        //        {
        //            if (ON)
        //            {
        //                if (data[i] == 1)
        //                {
        //                    items = data[i + 1];

        //                    //Console.WriteLine(items);

        //                    mainData[num, count] = Encoding.UTF8.GetString(data, i + 2, items);

        //                    //Console.WriteLine(mainData[num, count]);

        //                    end = items;
        //                    ON = false;

        //                }
        //                else
        //                {
        //                    //mainData[num, count] = Encoding.UTF8.GetString(data, i, 0);
        //                    mainData[num, count] = "Nothing";

        //                    count += 1;
        //                }
        //            }
        //            else
        //            {
        //                if (through < end)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }



        //        }
        //        else if (dataType[count] == 6)
        //        {
        //            mainData[num, count] = data[i].ToString();

        //            count += 1;
        //        }
        //        else if (dataType[count] == 7)
        //        {
        //            byte by = data[i];

        //            mainData[num, count] = by.ToString();

        //            //Console.WriteLine(num + " " + count + " " + mainData[num, count]);
        //            count += 1;
        //        }
        //        else if (dataType[count] == 8)
        //        {
        //            sbyte sby = Convert.ToSByte(data[i]);

        //            mainData[num, count] = sby.ToString();

        //            count += 1;
        //        }
        //        else if (dataType[count] == 9)
        //        {
        //            if (ON)
        //            {
        //                short sho = BitConverter.ToInt16(data, i);

        //                mainData[num, count] = sho.ToString();

        //                end = 2;
        //                ON = false;
        //            }
        //            else
        //            {
        //                if (through < end - 1)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }

        //        }
        //        else if (dataType[count] == 10)
        //        {
        //            if (ON)
        //            {
        //                ushort sho = BitConverter.ToUInt16(data, i);

        //                mainData[num, count] = sho.ToString();

        //                end = 2;
        //                ON = false;
        //            }
        //            else
        //            {
        //                if (through < end - 1)
        //                {
        //                    through += 1;
        //                }
        //                else
        //                {
        //                    through = 0;
        //                    ON = true;
        //                    count += 1;
        //                }
        //            }
        //        }
        //        else if (dataType[count] == 0)
        //        {
        //            mainData[num, count] = "None";

        //            count += 1;
        //        }




        //        if (count >= readMax)
        //        {
        //            count = 0;
        //            num += 1;

        //            if (num == d_Length)
        //            {
        //                break;
        //            }
        //        }

        //    }

        //    //for(int i = 0; i < d_Length; i++)
        //    //{
        //    //    for (int k = 0; k < 21; k++)
        //    //    {
        //    //        Console.Write("" + mainData[i, k]);
        //    //    }

        //    //    Console.WriteLine("");
        //    //}

        //}


        //public static void Header_Read(byte[] data, int h_Length)
        //{

        //    int count = 0;

        //    int d_Length;


        //    //Console.WriteLine(h_Length);

        //    byte[] header = new byte[h_Length];

        //    for (int i = 0; i < h_Length; i++)
        //    {
        //        header[i] = data[i + 4];

        //        //Console.Write("{0,2:X2} ", header[i]);
        //    }


        //    d_Length = BitConverter.ToInt32(header, 0);

        //    //onsole.WriteLine("\n" + d_Length);

        //    head_Dat = new string[d_Length];
        //    dataType = new byte[d_Length];

        //    int num = 0;
        //    int items = 0;

        //    int through;
        //    int end = -1;

        //    for (int i = 4; i < h_Length; i++)
        //    {
        //        if (count == 0)
        //        {
        //            dataType[num] = header[i];

        //            //Console.WriteLine(header[i]);
        //            count += 1;
        //        }
        //        else if (count == 1)
        //        {
        //            items = header[i];

        //            count += 1;

        //            //Console.WriteLine(items);
        //        }
        //        else if (count == 2)
        //        {
        //            head_Dat[num] = Encoding.UTF8.GetString(header, i, items);

        //            //Console.WriteLine(head_Dat[num]);

        //            num += 1;
        //            count += 1;
        //        }
        //        else if (count == 3)
        //        {
        //            if (count > items)
        //            {
        //                count = 0;
        //            }
        //            else
        //            {
        //                through = items - 2;
        //                end = through + count;

        //                count += 1;
        //            }
        //        }
        //        else
        //        {
        //            if (count == end)
        //            {
        //                count = 0;
        //            }
        //            else
        //            {
        //                count += 1;
        //            }
        //        }

        //        //Console.WriteLine("count="+count);
        //    }

        //    for (int i = 0; i < d_Length; i++)
        //    {
        //        text += head_Dat[i];
        //        //Console.Write(head_Dat[i]);
        //        if (i != d_Length - 1)
        //        {
        //            //Console.Write(",");
        //            text += ",";
        //        }

        //    }

        //    File_Write(text);

        //    text = "";

        //    for (int i = 0; i < d_Length; i++)
        //    {


        //        string[] arr = new string[d_Length];

        //        if(dataType[i] == 0)
        //        {
        //            arr[i] = "None";
        //        }
        //        else if(dataType[i] == 1)
        //        {
        //            arr[i] = "Int";
        //        }
        //        else if (dataType[i] == 2)
        //        {
        //            arr[i] = "Long";
        //        }
        //        else if (dataType[i] == 3)
        //        {
        //            arr[i] = "Float";
        //        }
        //        else if (dataType[i] == 4)
        //        {
        //            arr[i] = "Double";
        //        }
        //        else if (dataType[i] == 5)
        //        {
        //            arr[i] = "String";
        //        }
        //        else if (dataType[i] == 6)
        //        {
        //            arr[i] = "Bool";
        //        }
        //        else if (dataType[i] == 7)
        //        {
        //            arr[i] = "Byte";
        //        }
        //        else if (dataType[i] == 8)
        //        {
        //            arr[i] = "SByte";
        //        }
        //        else if (dataType[i] == 9)
        //        {
        //            arr[i] = "Short";
        //        }
        //        else if (dataType[i] == 10)
        //        {
        //            arr[i] = "Ushort";
        //        }


        //        text += arr[i];

        //        if (i != d_Length - 1)
        //        {
        //            //Console.Write(",");
        //            text += ",";
        //        }

        //    }

        //    File_Write(text);
        //}



        //public static void File_Delete()
        //{
        //    using (var fileStream = new FileStream(fileName, FileMode.Open))
        //    {
        //        fileStream.SetLength(0);
        //    }
        //}
    }
}
