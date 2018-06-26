using MuslimSurvivalKit.Model;
using SQLite;
using System;
using System.Collections.Generic;
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

            database.CreateTablesAsync<AyahArOriginal, AyahEnSahih, Surah, Reciter>().Wait();
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
    }
}
