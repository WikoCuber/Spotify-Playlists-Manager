namespace SPM_Data
{
    public static class Cache
    {
        private static readonly string DIR = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SPM\Cache\";
        private static readonly ReaderWriterLockSlim locker; //For async

        static Cache()
        {
            locker = new();

            if (!Directory.Exists(DIR))
                Directory.CreateDirectory(DIR);

            if (!File.Exists(DIR + "list.txt"))
            {
                var file = File.Create(DIR + "list.txt");
                file.Close();
            }
        }

        public static byte[]? Get(string id)
        {
            locker.EnterReadLock();
            StreamReader sr = new(DIR + "list.txt");
            string? line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split(';');
                if (data[0] == id) //id;file
                {
                    sr.Close();
                    locker.ExitReadLock();
                    return File.ReadAllBytes(DIR + data[1]);
                }
            }

            sr.Close();
            locker.ExitReadLock();
            return null;
        }

        public static void Set(string id, byte[] bytes)
        {
            locker.EnterWriteLock();

            int count = File.ReadLines(DIR + "list.txt").Count();
            string fileName = count + ".data";

            //Write in list
            StreamWriter sw = new(DIR + "list.txt", true);
            sw.WriteLine(id + ";" + fileName);

            //Create file
            File.WriteAllBytes(DIR + count + ".data", bytes);

            sw.Close();
            locker.ExitWriteLock();
        }
    }
}
