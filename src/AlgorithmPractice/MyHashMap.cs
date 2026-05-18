using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AlgorithmPractice;

public class MyHashMap<TKey, TValue> where TKey : notnull {

    public int Size { get; set; }
    public int Entries { get; set; }
    public LinkedList<KeyValuePair<TKey, TValue>>[] EntryBuckets { get; set; }

    public MyHashMap ()
    {
        EntryBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[16];
        Size = EntryBuckets.Length;
    }
    
    public void Put(TKey key, TValue value)
    {
        int modulo = key.GetHashCode() % this.Size;
        if (EntryBuckets[modulo] == null)
        {
            EntryBuckets[modulo] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }
        EntryBuckets[modulo].AddLast(new KeyValuePair<TKey, TValue>(key, value));

        this.Entries++;
        if (this.Entries * 4 >= Size * 3)
        {
            this.Resize();
        }
    }
    
    public TValue Get(TKey key)
    {
        int modulo = key.GetHashCode() % this.Size;

    }
    
    public void Remove(TKey key)
    {
        
    }

    private void Resize()
    {
        if (this.Entries * 4 < Size * 3)
        {
            throw new ArgumentException("This method was called incorrectly, there is a bug in the hashmap");
        }
        // This is wrong, fix it
        var oldEntryBucket = new LinkedList<KeyValuePair<TKey, TValue>>(this.EntryBuckets);
        this.Size = this.Size * 2;
        foreach (LinkedList<KeyValuePair<TKey, TValue>> bucket in this.EntryBuckets)
        {
            if (bucket == null) continue;
            foreach (KeyValuePair<TKey, TValue> entry in bucket)
            {
                
            }
        }
    }
};