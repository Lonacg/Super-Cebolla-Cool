# SUPER CEBOLLA COOL

## Resumen
Super Cebolla ha salido de la tierra para recorrer con estilo los mundos de Mario. Su sueño es llegar al castillo de Peach y vivir un “felices para siempre” en su huerto. No está dispuesta a compartir espacio con setas y mucho menos permitirá que las tortugas devoren su preciosa melena. Cualquiera que se ponga en su camino… ¡sufrirá las consecuencias de su oloroso superpoder!

* Género: plataformas
* Plataforma: PC
* Público objetivo: destinado a los amantes de Mario en sus primeras versiones
* Nº jugadores: singleplayer 
* Estilo gráfico y referencias: New Super Mario Bros para Nintendo DS

<img src="PLATAFORMAS.png" style="zoom: 33%;" />

## Loops
### Loop principal
* Jugador:
   * Avanzar o retroceder a una velocidad constante tras pulsar una tecla 
   * Saltar hasta cierta altura tras pulsar una tecla
   * Recoger monedas cuando la toque, haciendo que el contador de monedas aumente y el objeto moneda se destruya
   * Verificar si el contador de monedas llegó a una cantidad específica para aumentar una vida
   * Evolucionar tras tocar un objeto gota de agua
   * Disparar tras pulsar una tecla y estar en la segunda evolución
   * El proyectil viajará a una velocidad constante y si impacta contra un enemigo lo destruye
* Enemigos:
   * Caminar a una velocidad constante en dirección contraria al jugador
   * Comprobar si toca a un jugador para dañarlo o hacerle perder (según el estado en que se encuentre)
* Condición de victoria:
   * Comprobar si el jugador cruzó la bandera.
* Cámara:
   * Seguir la posición del jugador ignorando los saltos.



### Loops secundarios
* El jugador puede romper bloques saltando debajo de ellos
* El jugador puede aumentar su velocidad de movimiento pulsando otra tecla

## Características del juego
* Espacio de juego: personajes en 3D con movimiento restringido a 2D
* Punto de vista: el personaje se mantiene centrado en el centro de la pantalla
* Movimiento de la camara: la camara sigue al personaje manteniendolo siempre centrado
### Jugador
* El jugador comienza teniendo x vidas, cuando muere vuelve al comienzo de la pantalla y se resta una
* Cada pantalla tiene un cronómetro
* El jugador puede saltar y moverse hacia delante o hacia atras
* El jugador puede coger monedas y eliminar enemigos
* Contador de monedas, x monedas se convierten en una vida extra.
* Hay zonas de caída en las que el jugador muere directamente.
* El jugador tiene 2 estados equivalentes a vida:
   * Estado 1 (Visualización sin pelo): si un enemigo le toca se muere.
   * Estado 2 (Visualización con pelo y capacidad de disparo): si un enemigo le toca se cambia a Estado 1 y se hace invulnerable durante x segundos (parpadeo del personaje)
* Salto y velocidad de movimiento definidos con inercia.
### Enemigos
* Su movimiento se activa cuando entran por 1ª vez en el campo de visión del jugador.
* Se mueven en una dirección y a una velocidad fijas.
* Si tocan al jugador cuando está en Estado 1, el jugador muere.
* Si tocan al jugador cuando está en Estado 2, el jugador cambia al Estado 1.
* Si el jugador está en Estado 2 y les dispara, se mueren.
* Tipos de enemigo:
   * Setas: cuando el jugador salta encima se eliminan.
   * Tortugas: cuando el jugador salta encima la primera vez se quedan inactivos durante 8 segundos. Si el jugador vuelve a saltar encima cuando están inactivos se eliminan.

### Powerups
* Dependen del Estado del jugador.
* Están dentro de bloques determinados y salen cuando el jugador choca en el bloque saltando desde abajo.
* Si el jugador está en Estado 1 saldrá una gota de agua.
* Si el jugador está en Estado 2 saldrá abono.
* Características:
   * Gota de agua: hace que el personaje cambie del Estado 1 al Estado 2.
   * Abono: hace que el disparo del personaje alcance más casillas

## Historia
¿?(Breve introducción a la narrativa del juego, fase a fase si las hubiera (que con el tiempo que hay no debería ser mucha))

Una cebolla cobró vida en una huerta y ahora se encuentra avanzando hacia adelante. Esperemos que tenga un buen destino y no hacer llorar a algún humano.





