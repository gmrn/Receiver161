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

        public Decoder() => db = new ApplicationContext();

        /// <summary>
        /// Create and return list with values and data for to draw UI Element
        /// </summary>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal List<Tuple<string, string, string, string>> GetListUIElements(Message item, byte[] data)
        {
            //get a data for a body's framecontent
            //LINQ request to db
            var _contents = from m in db.Messages
                            join c in db.Contents on m.Id equals c.Id_messages
                            where m.Id.Equals(item.Id)
                            select new { Id = c.Id, Title = c.Title, Type = c.Type, Offset = c.Offset };

            //get data in list format
            var list = new List<Tuple<string, string, string, string>>();

            foreach (var tuple in _contents)
            {
                //output value
                var vOut = this.ParseType(data, tuple.Type, tuple.Offset);


                //add to list not BIN type
                list.Add(new Tuple<string, string, string, string>(tuple.Title, tuple.Type, vOut.ToString(), tuple.Type));

                //BitArray to boolArray
                bool[] boolArray;
                try
                {
                    boolArray = new bool[(vOut as BitArray).Length];
                    (vOut as BitArray).CopyTo(boolArray, 0);
                }
                catch (Exception e)
                {
                    continue;
                }

                //join tables Contents and Binaries
                var _contents1 = from c in _contents
                                 join b in db.Binaries on c.Id equals b.Id_contents
                                 where c.Id.Equals(tuple.Id)
                                 select new { Id = b.Id, Title = b.Title, Type = b.Rule, View = b.View, Length = b.Number_bit };

                //iterator in boolArray
                int temp_i = 0;

                foreach (var temp_t in _contents1)
                {
                    //boolArray to intArray
                    var bool_data = boolArray.Skip(temp_i).Take(temp_t.Length).ToArray();
                    int[] int_data = ToIntArray(bool_data);

                    //To String
                    string s = string.Join("", int_data);

                    //join tables Binaries and Bins_extended
                    //search specific line in union table
                    var _contents2 = from c in _contents1
                                     join b in db.Bins_extended on c.Id equals b.Id_binaries
                                     where c.Id.Equals(temp_t.Id) && b.Data.Equals(s)
                                     select b.Text;

                    string text = null;
                    if (_contents2.Count<string>() != 0)
                        text = _contents2.First();

                    //add to list part of BIN type
                    if (temp_t.Title != null)
                        list.Add(new Tuple<string, string, string, string>(temp_t.Title, temp_t.View, s, text));

                    //move iterator
                    temp_i += temp_t.Length;
                }
            }

            return list;
        }

        /// <summary>
        /// Decode byte line ane return list with values
        /// </summary>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal List<string> GetListValues(Message item, byte[] data)
        {
            var m_contents = from m in db.Messages
                            join c in db.Contents on m.Id equals c.Id_messages
                            where m.Id.Equals(item.Id)
                            select new { Id = c.Id, Type = c.Type, Offset = c.Offset };

            var listOut = new List<string>();

            foreach (var tuple in m_contents)
            {
                //output value
                var vOut = this.ParseType(data, tuple.Type, tuple.Offset);

                //add to list not BIN type
                listOut.Add(vOut.ToString());

                //BitArray to boolArray
                bool[] boolArray;
                try
                {
                    boolArray = new bool[(vOut as BitArray).Length];
                    (vOut as BitArray).CopyTo(boolArray, 0);
                }
                catch (Exception e)
                {
                    continue;
                }

                var c_binaries = from c in m_contents
                                 join b in db.Binaries on c.Id equals b.Id_contents
                                 where c.Id.Equals(tuple.Id)
                                 select new { Id = b.Id, Type = b.Rule, Length = b.Number_bit };

                //iterator in boolArray
                int iterator = 0;

                foreach (var tuple1 in c_binaries)
                {
                    //boolArray to intArray
                    var bool_data = boolArray.Skip(iterator).Take(tuple1.Length).ToArray();
                    int[] int_data = ToIntArray(bool_data);

                    //To String
                    string value = string.Join("", int_data);

                    //add to list part of BIN type
                    if (tuple1.Type != null)
                        listOut.Add(value);

                    //move iterator
                    iterator += tuple1.Length;
                }
            }
            return listOut;
        }

        /// <summary>
        /// Create and return list with data for to draw UI Element, not content values
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal List<Tuple<string, string, string>> GetListUIElements(Message item)
        {
            var m_contents = from m in db.Messages
                            join c in db.Contents on m.Id equals c.Id_messages
                            where m.Id.Equals(item.Id)
                            select new { Id = c.Id, Title = c.Title, Type = c.Type};

            var list = new List<Tuple<string, string, string>>();

            foreach (var tuple in m_contents)
            {

                list.Add(new Tuple<string, string, string>(tuple.Title, tuple.Type, tuple.Type));

                var c_binaries = from c in m_contents
                                 join b in db.Binaries on c.Id equals b.Id_contents
                                 where c.Id.Equals(tuple.Id)
                                 select new { Id = b.Id, Title = b.Title, Type = b.Rule, View = b.View };

                foreach (var tuple1 in c_binaries)
                {
                    var b_bin_ext = from c in c_binaries
                                     join b in db.Bins_extended on c.Id equals b.Id_binaries
                                     where c.Id.Equals(tuple1.Id)
                                     select b.Text;

                    string text = null;
                    if (b_bin_ext.Count<string>() != 0)
                        text = b_bin_ext.First();

                    //add to list part of BIN type
                    if (tuple1.Title != null)
                        list.Add(new Tuple<string, string, string>(tuple1.Title, tuple1.View, text));
                }
            }
            return list;
        }

        /// <summary>
        /// Parse element of byte array and return object of a value
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="type"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private object ParseType(byte[] buffer, string type, int offset)
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

        private int[] ToIntArray(bool[] data)
        {
            var intArray = new int[data.Length];
            for (int i = 0; i < intArray.Length; i++)
                intArray[i] = Convert.ToInt32(data[i]);

            return intArray;
        }
    }
}
