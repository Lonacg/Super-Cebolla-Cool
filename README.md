# Proyecto de Unity

Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.



## Reglas de escritura
- El máximo número de caracteres para una línea está limitada a 80 (El editor de código es capaz de mostrar una guía para respetar este límite).

- Todos los atributos de clase deben declararse al principio y debajo de la llave de apertura `{`

- Todos los métodos propios de Unity como `start()`, `update()`, `FixedUpdate()`, etc... deben declararse antes que los métodos nuestros.

- Los comentarios son imprescindibles para explicar qué hace cada parte que pueda interpretarse de forma errónea.

- Las variables se deben declarar en camelCase
	```
	addForce
	destroyObject
	cubeDimension
	```
	
- Las constantes se deben declarar en mayúsculas y como variables globales (atributos de clase)
	```
	MAXVELOCITY
	PI
	```
	
- Todas las variables deben declararse en private excepto aquellas que deben aparecer en el editor de Unity, en ese caso deben ser public.

- Todos los métodos de clase deben declararse en private excepto aquellas que sean usadas fuera de la clase.

- Todas las funciones, métodos, condicionales y bucles (control de flujo) deben ser representadas de las siguiente forma.
	```
	if  (true)
	{
		// ...
	}
	```
	
- Si la condicional es una sola línea se permite omitir las llaves
	```
	if (true)
		// ...
	```



## Mecanicas

### Mecánicas de movimiento a velocidad normal (Hecho)

1. Pulsa un botón "->" y el personaje se mueve, al dejar de presionar ocurre una inercia en su dirección y se detiene
2. Pulsa un botón "->" y el personaje se mueve, deja de pulsar y pulsa el botón contrario "<-":
   - opcion facil(HECHA): s olvida de la inercia que tuviera e inicia el movimiento de la segunda flecha pulsada como en el caso 1
   - opcion dificil: cuando inicia el movimiento de la segunda flecha, mantiene la fuerza de inercia que le quedara del primer movimiento, para que le cueste mas arrancar
3. Pulsa un boton "->" y sin soltar pulsa "<-":
     - opcion 1: ignorar la segunda flecha pulsada, solo hace caso a la primera, mantiene igual que llevaba el movimeinto

     - opcion 2(HECHA): deja de moverse, como si hubieras soltado las 2 flechas
     
      	4. Pulsar las dos flechas al mismo tiempo: no se mueve


### Mecanicas de movimiento a velocidad aumentada (Hecho)
Mismos casos que a velocidad normal, la inercia es proporcional a la velocidad que llevase en el momento de soltar la tecla de movimiento

### Mecánica del salto
- Implementar el salto del profe, cuanto mas pulses la tecla para saltar, mas alto salta
- Extra: opciones de caida:
	- si estamos en el aire y pulsamos la flecha hacia abajo cae cn mas fuerza a rapida velocidad
	- si estamos en el aire y pulsamos la flecha cae con aceleracion, y si pulsamos otra tecla impacta en el suelo
- Si saltas en una direccion y en el aire pulsas la otra direccion cambia de direccion a la segunda que pulsas
- Incluir la interpolacion en el salto para verlo mas fluido

### Mecanica de la camara
- La camara se mueve en la direccion x del personaje manteniendolo centrado, independientemente de la velocidad que lleve
- La camara no se mueve en el eje y, a menos que el personaje alcance cierta altura, en ese caso le seguiria y le centraria
- La distancia en z es cte durante toda la partida

### Mecanica de enemigos
- Ambos enemigos, al tocar con una pared se dan la vuelta
- Seta: si se encuentran con un vacio se caen por el
- Tortuga: si se encuentra con un vacio se da la vuelta
- Si se tocan 2 enemigos ambos cambian de direccion
- Si el enemigo toca al jugador a los lados:
	- Si esta en el estado 1: se acaba la partida
	- Si esta en el estado 2: cambia al estado 1 y se vuelve invulnerable X segundos
- Si el jugador cae encima del enemigo:
	- Setas: se destruyen
	- Tortuga: se queda inmovilizada durante x segundos, si el jugador vuelve a caer encima mientras esta inmovilizada, la tortuga se elimina

## Powerups
- La visualizacion  del estado se hace teniendo 2 modelos diferentes, segun el estado se sustituye uno por otro en la posicion que estuviera
- Cuando se cambia de estado aparece polvo o una nube para disimular la transicion
- Cuando sale la gota de agua, se va en direccion contraria al jugador, acelerando ligeramente cuando esta muy cerca
- La gota de agua cambia al personaje del Estado 1 al Estado 2
- Cuando sale el abono: se queda quieto encima del bloque (opcional:sale disparado con direccion random (izq derecha) cerca del jugador)
- El abono potencia la distancia de disparo


## Backgrounds (fondo)
- Usando el efecto parallax donde los fondos siguen al personaje pero a una velocidad mas lenta segun mas profunda es la capa
- La altura de la imagen de cada capa es del tamaño de la pantalla de juego
- Cada imagen es un objeto (cadena de montañas, nube, conjunto de nubes,...)
- Cada imagen va en una capa para darle profundidad al fondo (varias cadenas de montañas y varias nubes)

## Log

#### 11 - 4 - 22

- Añadido modelo Cube dentro de carpeta Models y cuatro texturas difusas para el modelo Cube.

- Añadido cuatro materiales dentro de carpeta Materials para usar con el modelo cubo
  - Onion, Mushroom, Water, Turtle

#### 12 - 4 - 22

- Añadido clase Player que contiene el control del movimiento y del salto (incompleto)
  - Para moverse pulsar las teclas de flecha izquierda y derecha
  - Para saltar pulsar la tecla de flecha arriba

#### 13 - 4 - 22

- Renombrado método PlayerControl() a MovementControl()
- Añadido componente Box Collider a la malla default del gameObject Onion (Onion → Model → default)
- Añadido componente RigidBody al gameObject Onion
- Añadido componente Box Collider al gameObject Floor
- Añadido restricción en la posición del eje Z para el gameObject Onion
- Añadido restricción en la rotación del eje X, Y, Z para el gameObject Onion
- Añadido control de la aceleración pulsando la tecla Shift
- Los siguientes valores para el Script player han sido establecidos en:
	- Speed: 5
	- MaxSpeed: 12
	- Acceleration: 8
	- Drag: 0.94
	- JumpForce: 1000
	- Gravity: 48

#### 14 - 4 - 22
- Añadida inercia al movimiento horizontal
- Pendiente de corregir: pulsando una flecha+shift acelera, pero si dejas de pulsar la flecha mantiene la aceleracion

#### 15 - 4 - 22
- En un intento de reorganizar el código para separar las funciones del personaje por separado se ha llegado a lo siguiente (pendiende de revisión)
- Añadido método GetDirection(char axis) para obtener directamente la dirección tras pulsar una tecla
- Deshabilitado temporalmente control del salto
- El método MovementControl() temporalmente contiene la inercia, aceleración y movimiento

#### 16 - 4 - 22
- Arreglado el error de que mantenia la acelaracion al soltar el shift (else currSpeed=speed;)
- Corregido getDirection para que cuando pulsamos las 2 flechas no se mueva (antes cuando pulsamos <- y sin soltar pulsamos ->, cambiaba de direccion a ->)

#### 17 - 4 - 22
- Limpieza de comentarios
- Se ha vuelto a implementar el control del salto y eliminado un condicional innecesario
- Las nuevas implementaciones con el salto están dentro del método `JumpControl()`
- Implementado nueva variable directionDrag y ahora disponible desde el editor de Unity
- Implementado dos nuevas variables para el control del salto disponibles desde el editor de Unity:
	- liftingSpeed: la velocidad de elevación
		> Es también el control para la altura del salto
	- fallingSpeed: la velocidad de caída

#### 6 - 5 -22

- Añadido rotación del modelo con Slerp()

#### 07 - 05 - 22
- Añadidos prefab de los 4 tipos de bloques
- Añadida camara virtual de cinemachine, pte limitar el escenario cuando se cree
- Escala del modelo del personaje reducida para mantener los objetos nuevos a 1

#### 11 - 5 - 22

- Reprogramado las mecánicas de movimiento y salto para un control más exacto

- Las nuevas mecánicas de movimiento ahora tienen un modo velocidad normal y otro de alta velocidad.
  Cada modo tiene tres opciones personalizables:

  - Velocidad
  - Aceleración
  - Desaceleración

- Las nuevas mecánicas de salto ahora tienen mayor control y más preciso a diferencia de la anterior versión, incluyendo:

  - Tiempo de salto si se sigue manteniendo pulsado el botón de salto
  - Fuerza del salto
  - Fuerza del salto si se sigue mantiendo pulsado el botón de salto
  - Fuerza de caída una vez la velocidad en Y sea <= 0
  - Fuerza de gravedad

- Por razones de optimización se ha establecido el Rigidbody de player con los siguientes cambios:

  - Se usa la interpolación para evitar conflicos entre update y fixedupdate
  - La detección de colisión se ha establecido en Continuo, pero para mejores resultados debería dejarse en Continuo Especulativo.

- El personaje es capaz de rotar mirando hacia el sentido en donde avanza. La velocidad de rotación se ha establecido en 920.


#### 12 - 5 - 22

- Implementado intento en evitar quedarse pegado a las paredes o al techo. Si bien funciona aún está pendiente de corregir.
- Detectado nuevo bug al pulsar la tecla de movimiento en una dirección junto a shift y al dejar de presionarlas al mismo tiempo y pulsar la tecla de movimiento en dirección contraria acelera al personaje al infinito.

#### 13 - 05- 22

- Script de camara creado gestionado en conjunto con cinemachine, pte de terminar.
- Puesta la rotacion del personaje en el hijo.
- Condiciones de movimiento modificadas (lineas 67 y 70) para que player no se mueva cuando se pulsen las 2 flechas

#### 14 - 05 - 22

- Completado el correcto funcionamiento de la camara.
- Bug encontrado, cuando el jugador se choca con una pared  y sigue pulsando en esa direccion, la camara sigue avanzando porque la velocidad del jugador es positiva aunque no se mueva.

#### 18 - 05 - 22

- Ahora el personaje no se queda pegado en las paredes, suelo o bloques
- Implementado el disparo con proyectiles con los siguientes parámetros:
  - Tiempo entre disparos
  - Impulso horizontal del proyectil
  - Impulso vertical del proyectil
  - Tiempo de vida del proyectil (dentro del prefab proyectil)
  - Número de rebotes del proyectil (dentro del prefab proyectil)
- Detectado cierta vibración en los proyectiles (cuando el jugador salta y dispara). Dejado como está porque me gusta.
- Implementado el prefab monedas
- Añadido nuevas etiquetasbullet y coin
- Implementado vidas del jugador, contador de monedas y máximo número de monedas.
- Implementado colisión con los bloques desde abajo  e identificación del tipo de bloque golpeado.

#### 23 - 05 - 22
- Implementado movimiento de enemigos. Se mueven a velocidad cte y cuando se chocan con un objeto u otro enemigo se dan la vuelta.
- Si el jugador se choca con ellos por delante, siguen su camino, si se choca con ellos por detras cambian de direccion para mirar al jugador y siguen andando.
- Detectan cuando el jugador salta encima suya, pte hacer que el champi y el caparazon se destruyan y la tortuga cambie de estado.

#### 28 - 05 - 22
- Creados prefabs de bloques, monedas, enemigos y objetos del escenario.
- Montada escena con el escenario final, falta poner vegetacion en z=1.
- Ajustada la altura de Player y Turtle a 2 para que todo sea proporcional, y cambiado el Jump Force de Player de 30 a 28 para ajustar la altura del salto respecto a la escena montada.
- Compartidos bugs encontrados por discord:
	- No puede saltar si no esta tocando el suelo (Hace saltos encadenados si vuelves a pulsar el salto mientras esta en el aire)
	- Opcional: pulsar la tecla de salto una vez hace que solo salte una vez (manteniendo pulsadado la tecla ahora vuelve a saltar cuando llega al suelo). A mi me gustaria mas asi, pero como quieras
	- Las monedas no deben parar el movimiento que llevara Player cuando las toca
	- El disparo no se ve bien. He visto que en el juego limitan el maximo de bolas disparadas vivas en la escena a 2, por si quieres ponerlo asi, a mi me da igual

#### 30 - 05 - 22
- Los enemigos estan quietos en su posicion origen hasta que player se acerca lo suficiente y empiezan a andar. Cuando se aleja, y despues vuelve a acercarse, los enemigos comenzarán a moverse desde su posicion origen en direccion al jugador.

#### 01 - 06 - 22
- Añadido detección de colisiones de player con enemigos, el vacío, powerUps y monedas
- Añadido condiciones para evolucionar, game over, muerte, contador de monedas y de vidas y gestor de evolución.

#### 05 - 06 - 22
- Añadido sistema de partículas invocadas al tocar monedas
- Cuando el jugador muere tras tocar el abismo hace respawn a la ubicación original -> No debe hacer respawn, si no llamar al menu de la UI que aun no esta hecho
- Detectado problema de cámara con los movimientos en vertical, no se desplaza con suavidad ->La camara no se desplaza nunca en los movimientos en vertical, solo sigue en horizontal
- Detectado problema de cámara cuando el jugador hace respawn, no se alinea correctamente -> No debe hacer respawn directamente, recargando la escena desde la UI al pulsar "retry" deberia salir bien
- Detectado problema de cámara al inicio del juego, un breve parpadeo ocurre -> solucionado
- Ajustados parámetros de los proyectiles

#### 06 - 06 - 22
- Mecanicas de enemigos actualizada: creados scripts de Turtle y Mushroom que heredan de Enemy.
- Empiezan a moverse cuando el jugador esta cerca
- Cuando el jugador esta lejos los enemigos vuelven a su posicion original, orientandose hacia donde este el jugador
- El enemigo tortuga se da la vuelta cuando no hay suelo por el que seguir
- Solucionado error en la camara y matices en los que no son errores

#### 08 - 06 - 22

- Añadido rebote de bloques sorpresa tras golpearlos por debajo
- Añadido rebote del jugador tras saltar sobre la cabeza de un enemigo
- Problema de pestañeo con la cámara al inicio del juego sigue persistente
- Añadido cambio de material de bloque sorpresa tras ser golpeado
- Bloques destructibles, rebotan y se parten en dos al ser golpeados y se destruyen
- Necesario reestructurar script bloques con instancias, será tratado después
- Detectado error con enemigos al quedarse atrapados en la esquina de un bloque, se vuelven epilépticos

