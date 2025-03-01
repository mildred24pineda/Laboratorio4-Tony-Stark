using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static string directorioBase = "LaboratorioAvengers";
    static string archivoInventos = Path.Combine(directorioBase, "inventos.txt");

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        try
        {
            if (!Directory.Exists(directorioBase))
                Directory.CreateDirectory(directorioBase);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear la carpeta del laboratorio: {ex.Message}");
        }

        MostrarMenu();

        Console.WriteLine("\nEscriba 'menu' para mostrarlo de nuevo cuando lo necesite.");
        string entrada;
        do
        {
            Console.Write("\nSeleccione una opción: ");
            entrada = Console.ReadLine() ?? "";

            try
            {
                if (int.TryParse(entrada, out int opcion))
                {
                    switch (opcion)
                    {
                        case 1: CrearArchivo(); break;
                        case 2:
                            Console.Write("Ingrese el nombre del invento: ");
                            string invento = Console.ReadLine() ?? "";
                            AgregarInvento(invento);
                            break;
                        case 3: LeerLineaPorLinea(); break;
                        case 4: LeerTodoElTexto(); break;
                        case 5: CopiarArchivo("Backup"); break;
                        case 6: MoverArchivo("ArchivosClasificados"); break;
                        case 7: CrearCarpeta("ProyectosSecretos"); break;
                        case 8: ListarArchivos(); break;
                        case 9: EliminarArchivo(); break;
                        case 10:
                            Console.Write("Ingrese el invento que desea eliminar: ");
                            string eliminar = Console.ReadLine() ?? "";
                            EliminarInvento(eliminar);
                            break;
                        case 0:
                            Console.WriteLine("Saliendo del programa...");
                            return;
                        default:
                            Console.WriteLine("Opción inválida, intente de nuevo.");
                            break;
                    }
                }
                else if (entrada == "menu")
                {
                    MostrarMenu();
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Escriba un número o 'menu'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

        } while (true);
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
        Console.WriteLine("1. Crear archivo de inventos");
        Console.WriteLine("2. Agregar un nuevo invento");
        Console.WriteLine("3. Leer archivo línea por línea");
        Console.WriteLine("4. Leer todo el texto del archivo");
        Console.WriteLine("5. Copiar archivo a 'Backup'");
        Console.WriteLine("6. Mover archivo a 'ArchivosClasificados'");
        Console.WriteLine("7. Crear carpeta 'ProyectosSecretos'");
        Console.WriteLine("8. Listar archivos en 'LaboratorioAvengers'");
        Console.WriteLine("9. Eliminar archivo (después de copia de seguridad)");
        Console.WriteLine("10. Eliminar un invento específico");
        Console.WriteLine("0. Salir");
    }

    static void CrearArchivo()
    {
        try
        {
            if (!File.Exists(archivoInventos))
            {
                File.WriteAllText(archivoInventos, "1. Traje Mark I\n2. Reactor Arc\n3. Inteligencia Artificial JARVIS\n");
                Console.WriteLine("Archivo creado exitosamente.");
            }
            else
            {
                Console.WriteLine("El archivo ya existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear el archivo: {ex.Message}");
        }
    }

    static void AgregarInvento(string invento)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(invento))
            {
                File.AppendAllText(archivoInventos, invento + "\n");
                Console.WriteLine("Invento agregado con éxito.");
            }
            else
            {
                Console.WriteLine("No se puede agregar un invento vacío.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar invento: {ex.Message}");
        }
    }

    static void LeerLineaPorLinea()
    {
        try
        {
            if (File.Exists(archivoInventos))
            {
                string[] lineas = File.ReadAllLines(archivoInventos);
                Console.WriteLine("Inventos:");
                foreach (string linea in lineas)
                {
                    Console.WriteLine(linea);
                }
            }
            else
            {
                Console.WriteLine("Error: El archivo no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }
    }

    static void LeerTodoElTexto()
    {
        try
        {
            if (File.Exists(archivoInventos))
            {
                string contenido = File.ReadAllText(archivoInventos);
                Console.WriteLine("Contenido del archivo:\n" + contenido);
            }
            else
            {
                Console.WriteLine("Error: El archivo no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }
    }

    static void CopiarArchivo(string destino)
    {
        try
        {
            string rutaDestino = Path.Combine(directorioBase, destino);
            if (!Directory.Exists(rutaDestino))
                Directory.CreateDirectory(rutaDestino);

            string archivoCopia = Path.Combine(rutaDestino, "inventos_backup.txt");
            File.Copy(archivoInventos, archivoCopia, true);
            Console.WriteLine("Archivo copiado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al copiar el archivo: {ex.Message}");
        }
    }

    static void MoverArchivo(string destino)
    {
        try
        {
            string rutaDestino = Path.Combine(directorioBase, destino);
            if (!Directory.Exists(rutaDestino))
                Directory.CreateDirectory(rutaDestino);

            string archivoMovido = Path.Combine(rutaDestino, "inventos.txt");
            File.Move(archivoInventos, archivoMovido, true);
            Console.WriteLine("Archivo movido exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al mover el archivo: {ex.Message}");
        }
    }

    static void EliminarArchivo()
    {
        try
        {
            CopiarArchivo("Backup");
            File.Delete(archivoInventos);
            Console.WriteLine("Archivo eliminado después de hacer copia de seguridad.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar el archivo: {ex.Message}");
        }
    }

    static void EliminarInvento(string nombreInvento)
    {
        try
        {
            if (File.Exists(archivoInventos))
            {
                List<string> lineas = File.ReadAllLines(archivoInventos).ToList();
                int eliminados = lineas.RemoveAll(linea => linea.ToLower().Contains(nombreInvento.ToLower()));

                if (eliminados > 0)
                {
                    File.WriteAllLines(archivoInventos, lineas);
                    Console.WriteLine($"Invento '{nombreInvento}' eliminado.");
                }
                else
                {
                    Console.WriteLine($"No se encontró el invento '{nombreInvento}'.");
                }
            }
            else
            {
                Console.WriteLine("Error: El archivo no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar invento: {ex.Message}");
        }
    }
    static void CrearCarpeta(string nombreCarpeta)
    {
        try
        {
            string rutaCarpeta = Path.Combine(directorioBase, nombreCarpeta);
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
                Console.WriteLine($"Carpeta '{nombreCarpeta}' creada exitosamente.");
            }
            else
            {
                Console.WriteLine($"La carpeta '{nombreCarpeta}' ya existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear la carpeta '{nombreCarpeta}': {ex.Message}");
        }
    }
    static void ListarArchivos()
    {
        try
        {
            if (Directory.Exists(directorioBase))
            {
                string[] archivos = Directory.GetFiles(directorioBase);
                Console.WriteLine("Archivos en 'LaboratorioAvengers':");
                foreach (string archivo in archivos)
                {
                    Console.WriteLine(Path.GetFileName(archivo));
                }
            }
            else
            {
                Console.WriteLine("Error: El directorio no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al listar los archivos: {ex.Message}");
        }
    }
}


