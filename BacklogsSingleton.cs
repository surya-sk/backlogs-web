using Microsoft.Graph;
using Newtonsoft.Json;
using BacklogsWeb.Graph;
using System.Text;

namespace BacklogsWeb
{
    class BacklogsSingleton
    {
        private static BacklogsSingleton instance = new BacklogsSingleton();
        private List<Backlog>? backlogs = null;
        string fileName = "backlogs.txt";
        private static GraphServiceClient? graphServiceClient = null;

        private BacklogsSingleton() { }

        public static BacklogsSingleton GetInstance()
        {
            return instance;
        }

        public async Task WriteDataAsync(GraphServiceClient graphClient)
        {
            string json = JsonConvert.SerializeObject(backlogs);
            graphServiceClient = graphClient;
            if (graphServiceClient is null)
                return;
            byte[] bytes = Encoding.ASCII.GetBytes(json);
            await graphServiceClient.Me.Drive.Root.ItemWithPath(fileName).Content.Request().PutAsync<DriveItem>(new MemoryStream(bytes));
        }

        public void SaveBacklogs(List<Backlog> _backlogs)
        {
            backlogs = _backlogs;
        }

        public async Task ReadDataAsync(GraphServiceClient graphClient)
        {
            try
            {
                string json = await DownloadBackogsAsync(graphClient);
                if(json != null)
                {
                    backlogs = JsonConvert.DeserializeObject<List<Backlog>>(json);
                }
            }
            catch
            {
                backlogs = new List<Backlog>();
            }
        }

        private async Task<string> DownloadBackogsAsync(GraphServiceClient graphClient)
        {
            graphServiceClient = graphClient;
            var search = await graphServiceClient.Me.Drive.Root.Search(fileName).Request().GetAsync();
            if(search.Count == 0)
            {
                return null;
            }
            using (Stream stream = await graphServiceClient.Me.Drive.Root.ItemWithPath(fileName).Content.Request().GetAsync())
            {
                using(StreamReader sr = new StreamReader(stream))
                {
                    string json = sr.ReadToEnd();
                    return json;
                }
            }
        }
    }
}
