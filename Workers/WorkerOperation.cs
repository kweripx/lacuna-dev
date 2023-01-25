namespace LacunaAdmission.Workers;

public class Job {
    public string Id { get;}
    public string Type {get; }
    public string? Strand { get; }
    public string? StrandEncoded { get;}
    public string? GeneEncoded { get;}

    public Job(string id, string type, string? strand, string? strandEncoded, string? geneEncoded) {
        Id = id;
        Type = type;
        Strand = strand;
        StrandEncoded = strandEncoded;
        GeneEncoded = geneEncoded; 
    }
    public override string ToString()
    {
        var str = $"JobOperation{{Id: {Id}, Type: {Type}";

        if (Strand != null) { str += $"Strand: {Strand}"; }

        if (StrandEncoded != null) { str += $"StrandEncoded: {StrandEncoded}"; }
        
        if (GeneEncoded != null) { str += $"GeneEncoded: {GeneEncoded}"; }

        return str + "}";
    }
    public static bool GeneCheck (string gene, string strand) {
        for (var i = 0; i < gene.Length; i++)
        for (var j = i; j < gene.Length; j++) {
            if ( j - i < (gene.Length / 2)) continue;
            if (j == i) continue;

            if (strand.Contains(gene.Substring(i, j - i))) { return true; }
        }
        return false;
    }

    public static string EncodedStrand(string strand) {
        var bytes  = new List<byte>();

        for (var j = 0; j < strand.Length; j += 4) {
            var codes = EncodeCode(strand[j]) << 2 * 3;
            codes |= EncodeCode(strand[j + 1]) << 2 * 2;
            codes |= EncodeCode(strand[j + 2]) << 2;
            bytes.Add((byte)codes);
        }
        return Convert.ToBase64String(bytes.ToArray());
    }
    public static string DecodeStrand(string strandEncoded) {
        var str = "";

        foreach (var i in Convert.FromBase64String(strandEncoded)) {
            for (var l = 3; l > 0; l--) { str += DecodeCode(0b11 & (i >> 2 * l));}   
        }

        return str;
    }
    private static byte EncodeCode(char code) => code switch {
        'A' => 0b00,
        'C' => 0b01,
        'G' => 0b10,
        'T' => 0b11,
        _ => throw new Exception($"Wasnt possible to encode code {code}")
    };
    private static char DecodeCode(int y) => y switch {
        0b00 => 'A',
        0b01 => 'C',
        0b10 => 'G',
        0b11 => 'T',
        _ => throw new Exception("Wasnt possible to decode code: " + Convert.ToString(y, 2).PadLeft(8, '0'))
    };
}