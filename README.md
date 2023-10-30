## AI Navigation 

Projekt polegał na dodaniu do istniejącego środowiska ludzika, który zbiera surowce z budynków i zanosi je w odpowiednie inne miejsce.

Zadanie zajęło mi około 3.5 godziny, ale wszystko działa jak powinno (a przynajmniej na tyle ile sprawdziłem) - to że ludzik czeka aż magazyn powstanie jest zamierzone 

Także ludzik chodzi po dynamicznie (wraz ze stawianiem budynków) navmapie i krąży między odpowiednimi budynkami roznosząc po jednym surowcu/produkcie

Poczyniłem kilka zmian w istniejącym kodzie, jak dodanie "rodzica" do budynków, sprawdzanie czy lista resourców jest pusta, czy niekrytyczny błąd przy dodawaniu surowców do listy

Dodałem też tagi do budyków, z których ostatecznie nie korzystałem.

# Co ewentalnie bym zmienił?
Dodałbym budowę ścieżki po której to ludzik mógłby się poruszać i trzebaby ją budować

Po kliknięciu na ludzika, jego ekwipunek jest zawsze widoczny

Ujednolicił budynki pod zwględem Outputu i Inputu (głównie o extractor mi chodzi)

Większą przejrzystość przy priorytezacji celu
