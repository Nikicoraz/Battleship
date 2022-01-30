using System;
using System.Linq;
using System.Text;

namespace Tabelle
{
    static class Tabelle
    {
        // Linee di border
        readonly static char H_LINE = '\u2550';
        readonly static char V_LINE = '\u2551';
        readonly static char UP_RIGHT_CORNER = '\u2557';
        readonly static char UP_LEFT_CORNER = '\u2554';
        readonly static char BOTTOM_LEFT_CORNER = '\u255A';
        readonly static char BOTTOM_RIGHT_CORNER = '\u255D';
        readonly static char TRI_INTERSECTION_LEFT = '\u2560';
        readonly static char TRI_INTERSECTION_RIGHT = '\u2563';
        readonly static char TRI_INTERSECTION_UP = '\u2569';
        readonly static char TRI_INTERSECTION_DOWN = '\u2566';
        readonly static char QUAD_INTERSECTION = '\u256C';

        public static char GetLetter(int index, bool capital = true)
        {
            int startIndex = (capital) ? 65 : 97;
            return (char)(index % 26 + startIndex);
        }

        public static string ColonnaSingola(string[] opzioni, bool numerata)
        {
            // Tipo reduce in javascript
            int maxLength = opzioni.Aggregate(0, (max, cur) => max > cur.Length ? max : cur.Length);
            // I due spazi all'inizio e fine
            maxLength += 3;
            if (numerata)
            {
                // Aggiungere lo spazio per i numeri e il punto e spazio
                maxLength += Convert.ToString(opzioni.Length).Length + 2;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < (3 + (2 * opzioni.Length - 2)); i++)
            {
                // Righe in cui si stampano caratteri speciali
                if (i % 2 == 0)
                {
                    // Prima riga
                    if (i == 0)
                    {
                        sb.Append(UP_LEFT_CORNER);
                        sb.Append(H_LINE, maxLength - 1);
                        sb.Append(UP_RIGHT_CORNER);
                    }
                    // Ultima riga
                    else if (i == (3 + (2 * opzioni.Length - 1)) - 2)
                    {
                        sb.Append(BOTTOM_LEFT_CORNER);
                        sb.Append(H_LINE, maxLength - 1);
                        sb.Append(BOTTOM_RIGHT_CORNER);
                    }
                    // Righe tra le opzioni
                    else
                    {
                        sb.Append(TRI_INTERSECTION_LEFT);
                        sb.Append(H_LINE, maxLength - 1);
                        sb.Append(TRI_INTERSECTION_RIGHT);
                    }
                }
                // Riga in cui si stampano le opzioni
                else
                {
                    string opzione = "";
                    opzione += V_LINE;
                    opzione += " ";
                    if (numerata)
                    {
                        // Numero dell'opzione
                        opzione += i - ((i - 1) / 2);
                        opzione += ".";
                        opzione += " ";
                    }
                    // Scritta dell'opzione
                    opzione += opzioni[i - ((i - 1) / 2) - 1];
                    sb.Append(opzione);
                    // Stampa degli spazi necessari per mantenere una lunghezza fissa
                    sb.Append(' ', maxLength - opzione.Length);
                    sb.Append(V_LINE);
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
        // TODO: molteplici colonne
        static public string Riquadro(int width, int height)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 && j == 0)
                        sb.Append(UP_LEFT_CORNER);
                    else if (i == 0 && j == width - 1)
                        sb.Append(UP_RIGHT_CORNER);
                    else if (i == height - 1 && j == 0)
                        sb.Append(BOTTOM_LEFT_CORNER);
                    else if (i == height - 1 && j == width - 1)
                        sb.Append(BOTTOM_RIGHT_CORNER);
                    else if (j == 0 || j == width - 1)
                        sb.Append(V_LINE);
                    else if (i == 0 || i == height - 1)
                        sb.Append(H_LINE);
                    else
                        sb.Append(" ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
        // Non funziona ancora grandezza celle
        static public string Griglia(int side, int grandezza_celle = 1, bool numerata = false, bool conLettere = false)
        {
            if (side < 1)
            {
                throw new Exception("Grid has to be at least 1 * 1!");
            }
            StringBuilder sb = new StringBuilder();
            int fullSide = side + (side + 1) * grandezza_celle;
            int gridStart = (numerata) ? 1 : 0;
            if (numerata)
                fullSide++;

            int celle = 1 + grandezza_celle;

            for (int i = 0; i < fullSide; i++)
            {
                bool primaRiga = i < gridStart;

                for (int j = 0; j < fullSide; j++)
                {
                    bool primaColonna = j < gridStart;
                    if (numerata)
                    {
                        if (j == 0 && (i - gridStart) % celle != 0 && !primaRiga)
                        {
                            if (conLettere)
                                sb.Append(GetLetter(side - (i - 1 - ((i - 1) / celle))));
                            else
                                sb.Append(i - 1 - ((i - 1) / celle));
                        }
                        if (i == 0)
                        {
                            if ((j - gridStart) % celle != 0 && j != 0)
                                sb.Append(j - 1 - ((j - 1) / celle));
                            else
                                sb.Append(" ");
                        }
                    }
                    // ANGOLI
                    if (i == gridStart && j == gridStart)
                        sb.Append(UP_LEFT_CORNER);
                    else if (i == gridStart && j == fullSide - 1)
                        sb.Append(UP_RIGHT_CORNER);
                    else if (i == fullSide - 1 && j == gridStart)
                        sb.Append(BOTTOM_LEFT_CORNER);
                    else if (i == fullSide - 1 && j == fullSide - 1)
                        sb.Append(BOTTOM_RIGHT_CORNER);
                    // MURA
                    else if ((i - gridStart) % celle == 0 && j == gridStart)
                        sb.Append(TRI_INTERSECTION_LEFT);
                    else if ((i - gridStart) % celle == 0 && j == fullSide - 1)
                        sb.Append(TRI_INTERSECTION_RIGHT);
                    else if (i == gridStart && ((j + gridStart) % celle == 0))
                        sb.Append(TRI_INTERSECTION_DOWN);
                    else if (i == fullSide - 1 && ((j + gridStart) % celle == 0))
                        sb.Append(TRI_INTERSECTION_UP);
                    else if ((i - gridStart) % celle == 0 && (j - gridStart) % celle == 0)
                        sb.Append(QUAD_INTERSECTION);
                    else if ((i - gridStart) % celle == 0 && ((j - gridStart) % celle != 0) && !primaColonna)
                        sb.Append(H_LINE);
                    else if ((j - gridStart) % celle == 0 && (i - gridStart) % celle != 0 && !primaRiga)
                        sb.Append(V_LINE);
                    else if (!primaRiga && !(j == 0 && (i - gridStart) % celle != 0))
                        sb.Append(" ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
