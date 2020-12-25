using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace ServiceExercise069
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITI_UMY
    {
        [OperationContract]
        [WebGet(UriTemplate = "Mahasiswa", ResponseFormat = WebMessageFormat.Json)] // Untuk membuat slash, selalu relative
        List<Mahasiswa> GetAllMahasiswa(); // Mendapatkan kumpulan mahasiswa / seluruh data mahasiswa

        [OperationContract]
        [WebGet(UriTemplate = "Mahasiswa/{nim}", ResponseFormat = WebMessageFormat.Json)] // untuk get
        Mahasiswa GetMahasiswaByNIM(string nim); //mengambil data berdasarkan nim

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreateMahasiswa", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateMahasiswa(Mahasiswa mhs);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "DeleteMahasiswa/{nim}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string DeleteMahasiswa(string nim);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "UpdateMahasiswaByNIM", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateMahasiswaByNIM(Mahasiswa mhs);



    }

    [DataContract]
    public class Mahasiswa
    {
        //Adalah konversi atau kesepakatan //variable lokal
        private string _nama, _nim, _prodi, _angkatan;
        [DataMember(Order = 1)] //mengirim data untuk mengurutkan
        public string nama
        {
            get { return _nama; }
            set { _nama = value; }
        }
        [DataMember(Order = 2)]
        public string nim
        {
            get { return _nim; }
            set { _nim = value; }
        }
        [DataMember(Order = 3)]
        public string prodi
        {
            get { return _prodi; }
            set { _prodi = value; }
        }
        [DataMember(Order = 4)]
        public string angkatan
        {
            get { return _angkatan; }
            set { _angkatan = value; }
        }
    }
}
