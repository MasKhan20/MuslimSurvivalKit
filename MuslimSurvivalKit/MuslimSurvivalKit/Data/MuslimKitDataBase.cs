using MuslimSurvivalKit.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MuslimSurvivalKit.Data
{
    public class MuslimKitDataBase
    {
        readonly SQLiteAsyncConnection database;

        public MuslimKitDataBase(string databasepath)
        {
            database = new SQLiteAsyncConnection(databasepath);

            Init();
        }

        private async void Init()
        {
            await database.CreateTablesAsync<AyahArOriginal, AyahEnSahih, Surah, Reciter, Bookmark>();

            var last = await GetLast();

            if (last == null || last == default(Bookmark))
            {
                await database.InsertAsync(new Bookmark()
                {
                    BookmarkType = BookmarkType.Last,
                    SurahId = 1,
                    AyahId = 1,
                    PageNumber = 1,
                    Description = "Recent Page",
                });
            }
        }

        public Task<List<Surah>> GetSurahList()
        {
            return database.Table<Surah>()
                .OrderBy(s => s.PageNumber)
                .OrderBy(s => s.SurahId)
                .ToListAsync();
        }

        public Task<Surah> GetSurah(int surahId)
        {
            return database.Table<Surah>()
                .Where(s => s.SurahId == surahId)
                .FirstOrDefaultAsync();
        }

        public Task<List<AyahArOriginal>> GetArSurahByAyahs(int surahId)
        {
            return database.Table<AyahArOriginal>()
                .Where(a => surahId == a.SurahId)
                .OrderBy(a => a.AyahId)
                .ToListAsync();
        }

        public Task<AyahArOriginal> GetAyah(int surahId, int ayahId)
        {
            return database.Table<AyahArOriginal>()
                .Where(a => a.SurahId == surahId)
                .Where(a => a.AyahId == ayahId)
                .FirstOrDefaultAsync();
        }

        public Task<AyahEnSahih> GetAyahEnSahih(int surahId, int ayahId)
        {
            return database.Table<AyahEnSahih>()
                .Where(a => a.SurahId == surahId)
                .Where(a => a.AyahId == ayahId)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetSurahFullName(int surahId)
        {
            var surah = await database.Table<Surah>()
                .Where(s => s.SurahId == surahId)
                .FirstOrDefaultAsync();
            return $"{surah.EnArName} - {surah.ArabicName}";
        }

        public Task<int> GetAyahCount(int surahId)
        {
            return database.Table<AyahArOriginal>()
                .Where(a => a.SurahId == surahId)
                .CountAsync();
        }

        public Task<List<Reciter>> GetReciters()
        {
            return database.Table<Reciter>()
                .OrderBy(r => r.FullName)
                .ToListAsync();
        }

        public Task<List<Bookmark>> GetBookmarks()
        {
            return database.Table<Bookmark>()
                .Where(b => b.BookmarkType != BookmarkType.Last)
                .OrderBy(b => b.DateCreated)
                .ToListAsync();
        }

        public async Task AddBookmark(Bookmark bookmark)
        {
            await database.InsertAsync(bookmark);
        }

        public async Task DeleteBookmark(Bookmark bookmark)
        {
            await database.DeleteAsync(bookmark);
        }

        public Task<Bookmark> GetLast()
        {
            return database.Table<Bookmark>()
                .Where(b => b.BookmarkType == BookmarkType.Last)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateLastId(int surahId, int ayahId)
        {
            var last = await database.Table<Bookmark>()
                .Where(b => b.BookmarkType == BookmarkType.Last)
                .FirstOrDefaultAsync();

            last.SurahId = surahId;
            last.AyahId = ayahId;
            last.DateCreated = DateTime.Now;

            await database.UpdateAsync(last);
        }

        public async Task UpdateLastPage(int pageNumber)
        {
            var last = await database.Table<Bookmark>()
                .Where(b => b.BookmarkType == BookmarkType.Last)
                .FirstOrDefaultAsync();

            last.PageNumber = pageNumber;
            last.DateCreated = DateTime.Now;

            await database.UpdateAsync(last);
        }
    }
}
