using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Sistema
    {
        public class OrdenarUsuarios : IComparer<Usuario>
        {
            public int Compare(Usuario? x, Usuario? y)
            {
                if (x == null || y == null)
                {
                    throw new Exception("Alguno de los valores es nulo");
                }
                return x.Email.CompareTo(y.Email);
            }
        }

        #region Instancia y precarga

        private static Sistema instancia;

        public static Sistema Instancia

        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Sistema();
                }
                return instancia;
            }
        }

        public void PreCargaReacciones()
        {
            Reaccion reaccion1 = new Reaccion();
            reaccion1.miembroReaccion = (Miembro)_usuarios[1];
            reaccion1.TipoReaccion = Reacciones.Like;
            Post post1 = _posts[0];
            CrearReaccion(reaccion1, post1);

            Reaccion reaccion2 = new Reaccion();
            reaccion2.miembroReaccion = (Miembro)_usuarios[2];
            reaccion2.TipoReaccion = Reacciones.Dislike;
            Post post2 = _posts[1];
            CrearReaccion(reaccion2, post2);

            Reaccion reaccion3 = new Reaccion();
            reaccion3.miembroReaccion = (Miembro)_usuarios[3];
            reaccion3.TipoReaccion = Reacciones.Like;
            Post post3 = _posts[2];
            CrearReaccion(reaccion3, post3);

            Reaccion reaccion4 = new Reaccion();
            reaccion4.miembroReaccion = (Miembro)_usuarios[4];
            reaccion4.TipoReaccion = Reacciones.Dislike;
            Post post4 = _posts[3];
            CrearReaccion(reaccion4, post4);
        }

        public void PrecargaDeDatos()
        {
            // 1 Administrador
            this.PreCargaAddUsuarioAdministrador("lucas@gmail.com", "hola1234");

            // 10 Miembros
            this.PreCargaAddUsuarioMiembro("jose@gmail.com", "hola1234", "jose", "rodgiruez", DateTime.UtcNow);
            this.PreCargaAddUsuarioMiembro("enrique@gmail.com", "hola1234", "enrique", "borgarello", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("joaquin@gmail.com", "hola1234", "joaquin", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("pedro@gmail.com", "hola1234", "pedro", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("alberto@gmail.com", "hola1234", "alberto", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("rodrigo@gmail.com", "hola1234", "rodrigo", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("gonzalo@gmail.com", "hola1234", "gonzalo", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("sofia@gmail.com", "hola1234", "sofia", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("camila@gmail.com", "hola1234", "camila", "gomez", DateTime.MinValue);
            this.PreCargaAddUsuarioMiembro("felipe@gmail.com", "hola1234", "felipe", "gomez", DateTime.MinValue);

            //5 posts
            Miembro autor1 = (Miembro)this._usuarios[1];
            Miembro autor2 = (Miembro)this._usuarios[2];
            Miembro autor3 = (Miembro)this._usuarios[3];
            Miembro autor4 = (Miembro)this._usuarios[4];
            Miembro autor5 = (Miembro)this._usuarios[5];

            this.AddPost(DateTime.Now, autor1, "post de jose", "un post de la vida de jose", "imagen.png", EstadoPost.PUBLICO);
            this.AddPost(DateTime.Now, autor2, "post de enrique", "un post de la vida de enrique", "imagen.png", EstadoPost.PUBLICO);
            this.AddPost(DateTime.Now, autor3, "post de joaquin", "un post de la vida de joaquin", "imagen.png", EstadoPost.PUBLICO);
            this.AddPost(DateTime.Now, autor4, "post de pedro", "un post de la vida de pedro", "imagen.png", EstadoPost.PUBLICO);
            this.AddPost(DateTime.Now, autor5, "post de alberto", "un post de la vida de alberto", "~/ imagenes / fotos / auto.jpg", EstadoPost.PUBLICO);

            //comentarios
            this.PreCargaAddComentario(0, "Esto es un comentario 0", DateTime.MinValue, autor2, "esto es un comentario 0");
            this.PreCargaAddComentario(0, "Esto es un comentario 1", DateTime.MinValue, autor2, "esto es un comentario 1");
            this.PreCargaAddComentario(0, "Esto es un comentario 2", DateTime.MinValue, autor2, "esto es un comentario 2");
            this.PreCargaAddComentario(0, "Esto es un comentario 3", DateTime.MinValue, autor2, "esto es un comentario 3");
            this.PreCargaAddComentario(0, "Esto es un comentario 4", DateTime.MinValue, autor2, "esto es un comentario 4");
            this.PreCargaAddComentario(3, "comentario 1", DateTime.Now, autor1, "esta muy bueno tu post lo que decis  es una realidad que nos afecta a todos");
            this.PreCargaAddComentario(3, "comentario 2", DateTime.Now, autor2, "lo que decis es una realidad que nos afecta a todos");
            this.PreCargaAddComentario(3, "comentario 3", DateTime.Now, autor3, "esta muy bueno tu post lo que decis ecta a todos");
            this.PreCargaAddComentario(3, "comentario 4", DateTime.Now, autor5, "esta muy bueno tu post lorealidad que nos afecta a todos");
            this.PreCargaAddComentario(3, "comentario 5", DateTime.Now, autor4, "esta muypost lo qe nos afecta a tos");
            this.PreCargaAddComentario(3, "comentario a censurar y usuario a bloquear", DateTime.Now, autor2, "horrible post muy malo bro deja de publicar tantas payasadas");
            this.PreCargaAddComentario(1, "comentario 3", DateTime.Now, autor4, "muy buena receta de cocina");
            this.PreCargaAddComentario(1, "comentario 4", DateTime.Now, autor5, "comentario de alberto en post de Pedro");

            //reacciones
            this.PreCargaReacciones();

            //invitaciones
            this.AddAmigo(autor1.Email, autor2.Email);
            this.AddInvitacion(DateTime.Now, autor2, autor1);
            this.AddInvitacion(DateTime.Now, autor3, autor1);
            this.AddInvitacion(DateTime.Now, autor1, autor4);
            this.AddInvitacion(DateTime.Now, autor1, autor5);
            this.AddInvitacion(DateTime.Now, autor5, autor1);

            //valor de aceptacion
            _posts[0].valorDeAceptacion = _posts[0].CalcularVA();
            _posts[1].valorDeAceptacion = _posts[1].CalcularVA();
            _posts[2].valorDeAceptacion = _posts[2].CalcularVA();
        }

        private Sistema()
        {
            PrecargaDeDatos();
        }
        #endregion

        #region Parametros
        private List<Usuario> _usuarios = new List<Usuario>();

        private List<Post> _posts = new List<Post>();

        private List<Comentario> _comentarios = new List<Comentario>();

        private List<Invitacion> _invitaciones = new List<Invitacion>();

        public List<Publicacion> _publicaciones = new List<Publicacion>();

        public Usuario usuarioLogueado = new Usuario();

        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public List<Comentario> GetComentarios()
        {
            return _comentarios;
        }

        public List<Invitacion> GetInvitacionesPorMiembro(Miembro miembro)
        {
            return miembro.GetListaPendientes();
        }
        #endregion

        #region Funcionalidad
        public void PreCargaAddUsuarioAdministrador(string email, string contrasenia)
        {
            Administrador administrador = new Administrador(email, contrasenia);
            _usuarios.Add(administrador);
        }

        public void PreCargaAddUsuarioMiembro(string email, string contrasenia, string nombre, string apellido, DateTime fechaNac)
        {
            Miembro miembro = new Miembro(email, contrasenia, nombre, apellido, fechaNac);
            _usuarios.Add(miembro);
        }

        public void AddUsuarioMiembro(Miembro miembro)
        {
            _usuarios.Add(miembro);
        }

        public void AddPost(DateTime fecha, Miembro autor, string titulo, string contenido, string imagen, EstadoPost estado)
        {
            Post post = new Post(fecha, autor, titulo, contenido, imagen, estado);
            this._posts.Add(post);
            this._publicaciones.Add(post);
            autor.cantidadDePublicaciones++;
        }

        public void PreCargaAddComentario(int idPost, string titulo, DateTime fecha, Miembro autor, string contenido)
        {
            Comentario comentario = new Comentario(titulo, fecha, autor, contenido);
            foreach (Post postAddComentario in this._posts)
            {
                if (postAddComentario.id == idPost)
                {
                    postAddComentario.GetComentarios().Add(comentario);
                }
            }
            this._comentarios.Add(comentario);
            this._publicaciones.Add(comentario);
            autor.cantidadDePublicaciones++;
        }

        public void AddComentario(int idPost, Comentario comentario, Miembro autor)
        {
            foreach (Post postAddComentario in this._posts)
            {
                if (postAddComentario.id == idPost)
                {
                    postAddComentario.GetComentarios().Add(comentario);
                }
            }
            this._comentarios.Add(comentario);
            this._publicaciones.Add(comentario);
            autor.cantidadDePublicaciones++;
        }

        public bool DoLogin(string email, string password)
        {
            bool result = false;
            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario.GetEmail().Equals(email))
                {
                    if (usuario.GetContrasenia().Equals(password))
                    {
                        this.usuarioLogueado = usuario;
                        result = true;
                    }
                }
            }
            return result;
        }

        public void AddInvitacion(DateTime fechaSolicitud, Miembro solicitante, Miembro solicitado, Estados estado = Estados.PENDIENTE_APROBACION)
        {
            Invitacion invitacion = new Invitacion(fechaSolicitud, solicitante, solicitado, estado);
            solicitado.GetListaPendientes().Add(invitacion);
        }

        public bool EsMiembro(string email)
        {
            bool result = false;
            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario.GetEmail().Equals(email))
                {
                    if (usuario.GetType().ToString().Contains("Miembro"))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool UsuarioYaSolicito(Miembro miembro, Miembro usuarioLogueado)
        {
            bool yaSolicito = false;
            foreach (Invitacion invitacion in miembro.GetListaPendientes())
            {
                if (usuarioLogueado.Equals(invitacion.Solicitante))
                {
                    yaSolicito = true;
                }
            }
            return yaSolicito;
        }

        public int GetPosicionEmal(string email)
        {
            int pos = 0;
            foreach (Usuario usuario in this._usuarios)
            {
                string usuarioEmail = usuario.GetEmail();
                if (usuarioEmail == email)
                {
                    return pos;
                }
                pos++;
            }
            return pos;
        }

        public Miembro GetUsuario(string? v)
        {
            foreach (Usuario u in _usuarios)
            {
                if (v == u.GetEmail())
                {
                    return (Miembro)u;
                }
            }
            return null;
        }

        public Usuario GetUsuarioUsuario(string? v)
        {
            foreach (Usuario u in _usuarios)
            {
                if (v == u.GetEmail())
                {
                    if (u.GetType().ToString().Contains("Administrador"))
                    {
                        return (Administrador)u;
                    }
                    else
                    {
                        return (Miembro)u;
                    }
                }
            }
            return null;
        }

        public List<Miembro> GetMiembros()
        {
            List<Miembro> listTempMiembros = new List<Miembro>();
            foreach (Usuario miembro in _usuarios)
            {
                if (EsMiembro(miembro.Email))
                {
                    listTempMiembros.Add((Miembro)miembro);
                }
            }
            return listTempMiembros;
        }

        public void AddPost(Post post)
        {
            this._posts.Add(post);
            this._publicaciones.Add(post);
        }

        public void AddInvitacion(Invitacion invitacion)
        {
            this._invitaciones.Add(invitacion);
        }

        public Invitacion EncontrarInvitacion(string usuarioSolicitado, string usuarioSolicitante)
        {
            Miembro miembroSolicitado = GetUsuario(usuarioSolicitado);
            Miembro miembroSolicitante = GetUsuario(usuarioSolicitante);
            foreach (Invitacion invitacion in GetInvitacionesPorMiembro(miembroSolicitado))
            {
                if (invitacion.Solicitado.Email == miembroSolicitado.Email
                && invitacion.Solicitante.Email == miembroSolicitante.Email)
                {
                    return invitacion;
                }
            }
            return null;

        }
        public void AddAmigo(string emailSolicitado, string emailSolicitante)
        {
            Miembro miembroSolicitado = GetUsuario(emailSolicitado);
            Miembro miembroSolicitante = GetUsuario(emailSolicitante);

            miembroSolicitado.GetListaAmigos().Add(miembroSolicitante);
            miembroSolicitante.GetListaAmigos().Add(miembroSolicitado);
        }

        public Post GetPostPorId(int IdPost)
        {
            Post postTemp = null;
            foreach (Post post in _posts)
            {
                if (post.id == IdPost)
                {
                    postTemp = post;
                }
            }
            return postTemp;
        }

        public void CrearReaccion(Reaccion nuevaReaccion, Publicacion publicacion)
        {
            if (publicacion.YaReacciono(nuevaReaccion.miembroReaccion))
            {
                Reaccion ultimaReaccion = publicacion.GetUltimaReaccion(nuevaReaccion.miembroReaccion);
                if (ultimaReaccion.TipoReaccion == nuevaReaccion.TipoReaccion)
                {
                    if (nuevaReaccion.TipoReaccion.Equals(Reacciones.Like))
                    {
                        publicacion.likes--;
                    }
                    else if (nuevaReaccion.TipoReaccion.Equals(Reacciones.Dislike))
                    {
                        publicacion.dislikes--;
                    }
                    int pos = publicacion.GetPosReaccion(ultimaReaccion.miembroReaccion);
                    publicacion.listaReacciones.RemoveAt(pos);
                }
                else if (ultimaReaccion.TipoReaccion != nuevaReaccion.TipoReaccion)
                {
                    if (nuevaReaccion.TipoReaccion.Equals(Reacciones.Like))
                    {
                        publicacion.likes++;
                        publicacion.dislikes--;
                        int pos = publicacion.GetPosReaccion(ultimaReaccion.miembroReaccion);
                        publicacion.listaReacciones.RemoveAt(pos);
                        publicacion.listaReacciones.Add(nuevaReaccion);
                    }
                    else if (nuevaReaccion.TipoReaccion.Equals(Reacciones.Dislike))
                    {
                        publicacion.dislikes++;
                        publicacion.likes--;
                        int pos = publicacion.GetPosReaccion(ultimaReaccion.miembroReaccion);
                        publicacion.listaReacciones.RemoveAt(pos);
                        publicacion.listaReacciones.Add(nuevaReaccion);
                    }
                }
            }
            else
            {
                if (nuevaReaccion.TipoReaccion == Reacciones.Like)
                {
                    publicacion.likes++;
                }
                else if (nuevaReaccion.TipoReaccion == Reacciones.Dislike)
                {
                    publicacion.dislikes++;
                }
                publicacion.listaReacciones.Add(nuevaReaccion);
            }
            publicacion.valorDeAceptacion = publicacion.CalcularVA();
        }

        public List<Publicacion> FiltrarPosts(string titulo, int VA)
        {
            List<Publicacion> listTemp = new List<Publicacion>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion.titulo.Contains(titulo) && publicacion.valorDeAceptacion > VA)
                {
                    listTemp.Add(publicacion);
                }
            }
            return listTemp;
        }

        public Comentario GetComentarioPorId(int idComentario)
        {
            foreach (Comentario comentario in this.GetComentarios())
            {
                if (comentario.id.Equals(idComentario))
                {
                    return comentario;
                }
            }
            return null;
        }

        public void OrdenarEmail()
        {
            _usuarios.Sort(new OrdenarUsuarios());
        }

        #endregion
    }
}