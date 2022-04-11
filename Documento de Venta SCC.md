# SUPER CEBOLLA COOL??

## Género y Plataforma Tecnológica
Juego de plataformas para PC, de un solo jugador.

## Protagonista e Historia
Super Cebolla ha salido de la tierra para recorrer con estilo los mundos de Mario. Su sueño es llegar al castillo de Peach y vivir un “felices para siempre” en su huerto. No está dispuesta a compartir espacio con setas y mucho menos permitirá que las tortugas devoren su preciosa melena. Cualquiera que se ponga en su camino… ¡sufrirá las consecuencias de su oloroso superpoder!


## Target y Referencias
Destinado a los amantes de Mario en sus primeras versiones, tomando como referencia fiel el New Super Mario Bros para Nintendo DS.

## Características del juego 
### Generales
* Cada pantalla tiene un cronómetro.
* Contador de monedas, x monedas se convierten en una vida extra.
* Hay zonas de caída en las que el jugador muere directamente.
* La cámara se mueve con el jugador para mantenerle siempre centrado.
* Los powerups que salen dependen del estado del jugador.
* El jugador empieza teniendo 5 vidas, cuando muere vuelve al comienzo de la pantalla y se resta una.

### Jugador
* Tiene 2 estados. 
   * Visualización sin pelo=estado 1 (si un enemigo le toca se muere).
   * Visualización con pelo y capacidad de disparo=estado 2 (si un enemigo le toca se cambia a la visualización de estado 1).
* Tras perder una vida, el personaje parpadea y se hace invulnerable durante 2 segundos.
* Salto y velocidad de movimiento definidos con inercia.
* Puede romper bloques chocando desde abajo al saltar.
* Cuando está en estado 2, la capacidad de disparar alcanza 2 casillas.

### Enemigos
* Su movimiento se activa cuando entran por 1ª vez en el campo de visión del jugador.
* Se mueven en una dirección y a una velocidad fijas.
* Si tocan al jugador cuando está en estado 1, el jugador muere.
* Si tocan al jugador cuando está en estado 2, el jugador cambia al estado 1.
* Si el jugador está en estado 2 y les dispara, se mueren.
* Tipos de enemigo:
   * Setas: cuando el jugador salta encima se eliminan.
   * Tortugas: cuando el jugador salta encima la primera vez se quedan inactivos durante 8 segundos. Si el jugador vuelve a saltar encima cuando están inactivos se eliminan.

### Powerups
* Están dentro de bloques determinados y salen cuando el jugador choca en el bloque saltando desde abajo.
* Si el jugador está en estado 1 saldrá una gota de agua.
* Si el jugador está en estado 2 saldrá abono.
* Características:
   * Gota de agua: hace que el personaje cambie del estado 1 al estado 2.
   * Abono: hace que el disparo de Super Cebolla alcance 3 casillas en vez de 2.