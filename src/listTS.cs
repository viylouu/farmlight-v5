partial class farmlight {
    class listTS<T> {
        T[] data;
        public int len;
        readonly object _lock = new object();

        public listTS() { data = Array.Empty<T>(); len = 0; }

        public void add(T item) {
            lock (_lock) {
                ensurecap(len+1);
                data[len] = item;
                len++;
            }
        }

        public void insert(T item, int idx) {
            lock (_lock) {
                if (idx < 0 || idx > len)
                    throw new ArgumentOutOfRangeException(nameof(idx), "Index is out of range.");

                ensurecap(len+1);
                for (int i = len; i > idx; i--)
                    data[i] = data[i-1];
                data[idx] = item;
                len++;
            }
        }

        public T this[int index] {
            get {
                lock (_lock) {
                    if (index < 0 || index >= len)
                        return default(T);
                    return data[index];
                }
            }
            set {
                lock (_lock) {
                    if (index < 0 || index >= len)
                    { }
                    data[index] = value;
                }
            }
        }

        void ensurecap(int min) {
            if (min > data.Length) {
                int newCapacity = data.Length * 2;
                if (newCapacity < min)
                    newCapacity = min;
                Array.Resize(ref data, newCapacity);
            }
        }
    }
}