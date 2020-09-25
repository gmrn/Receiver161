using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace Receiver161.PortServer
{
    class Decoder
    {
        ApplicationContext db;

        public Decoder() => db = ((App)Application.Current).db;

        /// <summary>
        /// Create and return list with values and data for to draw UI Element
        /// </summary>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        //internal List<Tuple<string, string, string, string>> GetListUIElements(Models.Message item, byte[] data)
        //{
        //    //get a data for a body's framecontent
        //    var contents = db.GetContentsForId(item.Id);

        //    //get data in list format
        //    var list = new List<Tuple<string, string, string, string>>();

        //    foreach (var tuple in contents)
        //    {
        //        //output value
        //        var vOut = this.ParseType(data, tuple.Type, tuple.Offset);

        //        //add to list not BIN type
        //        list.Add(new Tuple<string, string, string, string>(tuple.Title, tuple.Type, vOut.ToString(), tuple.Type));

        //        //BitArray to boolArray
        //        bool[] boolArray;
        //        try
        //        {
        //            boolArray = new bool[(vOut as BitArray).Length];
        //            (vOut as BitArray).CopyTo(boolArray, 0);
        //        }
        //        catch (Exception e)
        //        {
        //            continue;
        //        }

        //        //get records from Binaries
        //        var binaries = db.GetBinariesByResponseId(tuple.Id);

        //        //iterator in boolArray
        //        int iterator = 0;

        //        foreach (var tuple1 in binaries)
        //        {
        //            //boolArray to intArray
        //            var bool_data = boolArray.Skip(iterator).Take(tuple1.Number_bit).ToArray();
        //            int[] int_data = ToIntArray(bool_data);

        //            //To String
        //            string value = string.Join("", int_data);

        //            //join tables Binaries and Bins_extended
        //            //search specific line in union table
        //            var bin_ext = db.GetBinExtensionForId(tuple1.Id);

        //            string text = null;
        //            if (bin_ext.Count<Bin_extended>() != 0)
        //                text = bin_ext.First().Text;

        //            //add to list part of BIN type
        //            if (tuple1.Title != null)
        //                list.Add(new Tuple<string, string, string, string>(tuple1.Title, tuple1.View, value, text));

        //            //move iterator
        //            iterator += tuple1.Number_bit;
        //        }
        //    }

        //    return list;
        //}

        /// <summary>
        /// Decode byte line ane return list with values
        /// </summary>
        /// <param name="id_message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal List<string> GetListValues(int id_message, byte[] data)
        {
            var responses = db.GetResponsesById(id_message);
            var listOut = new List<string>();

            foreach (var tuple in responses)
            {
                //output value
                var vOut = this.ParseTypeToValue(data, tuple.Type, tuple.Offset);


                //BitArray to boolArray
                bool[] boolArray;
                try
                {
                    boolArray = new bool[(vOut as BitArray).Length];
                    (vOut as BitArray).CopyTo(boolArray, 0);
                }
                catch (Exception e)
                {
                    listOut.Add(vOut.ToString());
                    continue;
                }

                var binaries = db.GetBinariesByResponseId(tuple.Id);

                //iterator in boolArray
                int iterator = 0;

                foreach (var binary in binaries)
                {
                    //boolArray to intArray
                    var bool_data = boolArray.Skip(iterator).Take(binary.Length).ToArray();
                    int[] int_data = ToIntArray(bool_data);
                    Array.Reverse(int_data);

                    //To String
                    string value = string.Join("", int_data);
                    

                    //add to list part of BIN type
                    //if (tuple1.Rule != null)
                        listOut.Add(value);

                    //move iterator
                    iterator += binary.Length;
                }
            }
            return listOut;
        }

        /// <summary>
        /// Create and return list with data for to draw UI Element, not content values
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //internal List<Tuple<string, string, string>> GetListUIElements(Models.Message item)
        //{
        //    var contents = db.GetContentsForId(item.Id);
        //    var list = new List<Tuple<string, string, string>>();

        //    foreach (var tuple in contents)
        //    {
        //        list.Add(new Tuple<string, string, string>(tuple.Title, tuple.Type, tuple.Type));
        //        var binaries = db.GetBinariesByResponseId(tuple.Id);

        //        foreach (var tuple1 in binaries)
        //        {
        //            var bin_ext = db.GetBinExtensionForId(tuple1.Id);

        //            string text = null;
        //            if (bin_ext.Count<Bin_extended>() != 0)
        //                text = bin_ext.First().Text;

        //            //add to list part of BIN type
        //            if (tuple1.Title != null)
        //                list.Add(new Tuple<string, string, string>(tuple1.Title, tuple1.View, text));
        //        }
        //    }

        //    return list;
        //}

        internal byte[] WrapValuesToBytes(List<string> values, int id_message)
        {
            var legth = db.GetRequestsById(id_message).Count<Models.Request>();
            var buffer = new byte[legth * 2];

            var enumerator = values.GetEnumerator();

            foreach (var request in db.GetRequestsById(id_message))
            {
                if (enumerator.MoveNext())
                {
                    var subbuffer = ParseTypeToBytes(ref enumerator, request);

                    for (int i = 0; i < subbuffer.Length; i++)
                        buffer[request.Offset + i] = subbuffer[i];
                }
            }

            return buffer;
        }

        /// <summary>
        /// Parse element of byte array and return object of a value
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="type"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private object ParseTypeToValue(byte[] buffer, string type, int offset)
        {
            object vOut = null;

            //decoder byte[] data to value
            switch (type)
            {
                case ("UINT16"):
                    vOut = BitConverter.ToUInt16(buffer, offset);
                    break;
                case ("UINT32"):
                    vOut = BitConverter.ToUInt32(buffer, offset);
                    break;
                case ("SINT16"):
                    vOut = BitConverter.ToInt16(buffer, offset);
                    break;
                case ("SINT32"):
                    vOut = BitConverter.ToInt32(buffer, offset);
                    break;
                case ("FP"):
                    vOut = BitConverter.ToSingle(buffer, offset);
                    break;
                case ("EFP"):
                    vOut = BitConverter.ToDouble(buffer, offset);
                    break;
                case ("BIN16"):
                    vOut = new BitArray(
                        buffer.Skip(offset).Take(2).ToArray());
                    break;
                case ("BIN32"):
                    vOut = new BitArray(
                        buffer.Skip(offset).Take(4).ToArray());
                    break;
                case ("TIME"):
                    int[] intarr1 = { 0, 0, 0 };

                    for (int i = 0; i < intarr1.Length; i++)
                        intarr1[i] = Convert.ToInt32(buffer[offset + i]);
                    vOut = string.Join(":", intarr1);
                    break;
                case ("DATE"):
                    int[] intarr = { 2020, 0, 0 };

                    for (int i = 1; i < intarr.Length; i++)
                        intarr[i] = Convert.ToInt32(buffer[offset + i]);
                    vOut = string.Join("-", intarr);
                    break;
                default:
                    MessageBox.Show("Unknow type data");
                    break;
            }

            return vOut;
        }

        /// <summary>
        /// Translate the value to bytes in accordance with it's type
        /// </summary>
        /// <param name="enumerator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private byte[] ParseTypeToBytes(ref List<string>.Enumerator enumerator, Models.Request request)
        {
            var vOut = new byte[0];

            switch (request.Type)
            {
                case ("UINT16"):
                    vOut = BitConverter.GetBytes(
                        (UInt16.Parse(enumerator.Current)));
                    break;
                case ("UINT32"):
                    vOut = BitConverter.GetBytes(
                       (UInt32.Parse(enumerator.Current)));
                    break;
                case ("SINT16"):
                    vOut = BitConverter.GetBytes(
                        (Int16.Parse(enumerator.Current)));
                    break;
                case ("SINT32"):
                    vOut = BitConverter.GetBytes(
                        (Int32.Parse(enumerator.Current)));
                    break;
                case ("FP"):
                    vOut = BitConverter.GetBytes(
                        (float.Parse(enumerator.Current)));
                    break;
                case ("EFP"):
                    vOut = BitConverter.GetBytes(
                        (double.Parse(enumerator.Current)));
                    break;
                case ("BIN16"):
                    //tobitArray(ссылка на лист, указатель на текущее значение)
                    //toByteArray(bitArray[])
                    break;
                case ("BIN32"):
                    break;
                case ("TIME"):
                    break;
                case ("DATE"):
                    break;
                default:
                    break;
            }
            return vOut;

        }

        private int[] ToIntArray(bool[] data)
        {
            var intArray = new int[data.Length];
            for (int i = 0; i < intArray.Length; i++)
                intArray[i] = Convert.ToInt32(data[i]);

            return intArray;
        }
    }
}
