using System;
using System.Collections.Generic;

namespace MemCachDLL
{
    public class MemCach
    {
        /*
        * F I E L D S
        */
        private int sizeMax = 0;
        
        private Dictionary<string,string> cache;
        private LinkedList<string> indexList;
                       
        /*
        * C O N S T R U C T O R S
        */
        public MemCach(int size)
        {
            sizeMax = size;
            cache = new Dictionary<string, string>();
            indexList = new LinkedList<string>();
        }

        
       
        /*_______________________________________________________________________________________
        *
        *                            C R E A T E  M E T H O D S
        *
        *________________________________________________________________________________________*/
        /// <summary>
        /// add an entity in the cache, need a KeyValuePair<string,string> 
        /// </summary>
        /// <param name="kv"></param>
        public void Add(KeyValuePair<string, string> kv)
        {            
            if (!cache.ContainsKey(kv.Key) && kv.Key != null)
            {
                if (cache.Count == sizeMax)
                {
                    string lastKey = indexList.Last.Value;
                    indexList.Remove(lastKey);
                    cache.Remove(lastKey);

                }
                if(kv.Value == null || kv.Value.Equals(""))
                {
                    cache.Add(kv.Key, "UNKNOW");
                }
                else
                {
                    cache.Add(kv.Key, kv.Value);
                }
                indexList.AddFirst(kv.Key);
            }
        }

        /// <summary>
        /// add a group of entities into the cache, need a List of KeyValuePair<string,string>
        /// </summary>
        /// <param name="kvs"></param>
        public void AddAll(List<KeyValuePair<string,string>> kvs)
        {
            foreach (KeyValuePair<string, string> kv in kvs)
            {
                if (!cache.ContainsKey(kv.Key))
                {
                    if (cache.Count == sizeMax)
                    {
                        string lastKey = indexList.Last.Value;
                        indexList.Remove(lastKey);
                        cache.Remove(lastKey);
                    }
                    if( kv.Value == null || kv.Value.Equals(""))
                    {
                        cache.Add(kv.Key, "UNKNOW");
                    }
                    else
                    {
                        cache.Add(kv.Key, kv.Value);
                    }
                    indexList.AddFirst(kv.Key);
                }
            }      
        }
        
        /// <summary>
        /// java friendly additional method
        /// see Add()
        /// </summary>
        /// <param name="kv"></param>
        public void Put(KeyValuePair<string, string> kv)
        {
            if (!cache.ContainsKey(kv.Key))
            {
                if (cache.Count == sizeMax)
                {
                    string lastKey = indexList.Last.Value;
                    indexList.Remove(lastKey);
                    cache.Remove(lastKey);;
                }
                if(kv.Value.Equals("") || kv.Value == null)
                {
                    cache.Add(kv.Key, "UNKNOW");
                }
                else
                {
                    cache.Add(kv.Key, kv.Value);
                }
                indexList.AddFirst(kv.Key);
            }     
        }
        
        /// <summary>
        /// java friendly additional method
        /// see AddAll()
        /// </summary>
        /// <param name="kvs"></param>
        public void PutAll(List<KeyValuePair<string,string>> kvs)
        {
            foreach (KeyValuePair<string, string> kv in kvs)
            {
                if (!cache.ContainsKey(kv.Key))
                {
                    if (cache.Count == sizeMax)
                    {
                        string lastKey = indexList.Last.Value;
                        indexList.Remove(lastKey);
                        cache.Remove(lastKey);
                    }
                    if(kv.Value == null || kv.Value.Equals(""))
                    {
                        cache.Add(kv.Key, "UNKNOW");
                    }
                    else
                    {
                        cache.Add(kv.Key, kv.Value);
                    }
                    indexList.AddFirst(kv.Key);
                }
            }
            
        }
        
        /*_______________________________________________________________________________________
        *
        *                              R E A D  M E T H O D S
        *
        *________________________________________________________________________________________*/
        
        /// <summary>
        /// return a KeyValuePair<string,string> object where key and value are
        /// empty if the cache dont contains the specified key , otherwise with the found values
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValuePair<string,string> ContainsKey(string key)
        {
            if (cache.ContainsKey(key))
            {
                indexList.Remove(key);
                indexList.AddFirst(key);
                return new KeyValuePair<string, string>(key , cache[key]);
                
            }

            return new KeyValuePair<string, string>();
        }

        /// <summary>
        /// return a collection of whole cache keys
        /// </summary>
        /// <returns></returns>
        public List<string> KeySet()
        {
            List<string> keys = new List<string>();
            foreach (string key in cache.Keys)
            {
                keys.Add(key);
            }

            return keys;
        }

        /// <summary>
        /// return the newest key in cache
        /// </summary>
        /// <returns></returns>
        public string FirstKey()
        {
            if (indexList.Count > 0)
            {
                return indexList.First.Value;
            }

            return null;
        }
        
        /// <summary>
        /// return the newest value in cache
        /// </summary>
        /// <returns></returns>
        public string FirstValue()
        {
            if (indexList.Count > 0)
            {
                String key = indexList.First.Value;
                return cache[key];
            }

            return null;
        }
        
        /// <summary>
        /// retrurn the newest KeyValuePair object in cache
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> First()
        {
            if (indexList.Count > 0)
            {
                String key = indexList.First.Value;
                return new KeyValuePair<string, string>(key, cache[key]);
            }
            return new KeyValuePair<string, string>();
        }
        
        /// <summary>
        /// return the oldest key in cache
        /// </summary>
        /// <returns></returns>
        public string LastKey()
        {
            if (indexList.Count > 0)
            {
                return indexList.Last.Value;
            }

            return null;
        }
        
        /// <summary>
        /// return the oldest value in cache
        /// </summary>
        /// <returns></returns>
        public string LastValue()
        {
            if (indexList.Count > 0)
            {
                String key = indexList.Last.Value;
                return cache[key];
            }

            return null;
        }
        
        /// <summary>
        /// return the oldest KeyValuePair object in cache
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> Last()
        {
            if (indexList.Count > 0)
            {
                String key = indexList.Last.Value;
                return new KeyValuePair<string, string>(key, cache[key]);
            }
            
            return new KeyValuePair<string, string>();
        }

        /// <summary>
        /// return the whole cahce values
        /// </summary>
        /// <returns></returns>
        public List<string> ValueSet()
        {
            List<string> listValuesSet = new List<string>();
            foreach (string value in cache.Values)
            {
                listValuesSet.Add(value);
            }
            return listValuesSet;
        }

        /// <summary>
        /// print all cache KeyValuesPair objects in standard console out
        /// </summary>
        public void ToString()
        {
            foreach (string key in cache.Keys)
            {
                Console.WriteLine("{0} => {1}",key , cache[key]);
            }
        }
        
        
        /*_______________________________________________________________________________________
        *
        *                         U P D A T E  M E T H O D S
        *
        *________________________________________________________________________________________*/
        /// <summary>
        /// replace the KeyValuePair object value by the specified value
        /// </summary>
        /// <param name="kv"></param>
        public void Replace(KeyValuePair<string,string> kv)
        {
            if (cache.ContainsKey(kv.Key))
            {
                cache[kv.Key] = kv.Value;
            }
        }
        
        /*_______________________________________________________________________________________
        *
        *                          D E L E T E  M E T H O D S
        *
        *________________________________________________________________________________________*/
        /// <summary>
        /// remove the KeyValuePair object that match with the specified key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
                indexList.Remove(key);
            }
        }

        /// <summary>
        /// remove all cache keys and values
        /// </summary>
        public void RemoveAll()
        {
            indexList.Clear();
            cache.Clear();
        }
        /*_______________________________________________________________________________________
        *
        *                          C A C H E   P R O P E R T I E S 
        *
        *________________________________________________________________________________________*/
        /// <summary>
        /// return a bool true or false according to the cache is empty or not
        /// </summary>
        /// <returns></returns>
        public bool Empty()
        {
            if(cache.Count == 0)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// return the number of elements contained in cache
        /// </summary>
        /// <returns></returns>
        public int count()
        {
            return cache.Count;
        }

        /// <summary>
        /// return the maximal number of elements that the cache can contain
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return sizeMax;
        }
       
    }
}