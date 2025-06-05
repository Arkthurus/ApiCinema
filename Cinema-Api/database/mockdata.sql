-- Inserts para a tabela Ator
INSERT INTO Ator (Nome, DataNasc) VALUES
('Brad Pitt', '1963-12-18'),
('Angelina Jolie', '1975-06-04'),
('Leonardo DiCaprio', '1974-11-11'),
('Meryl Streep', '1949-06-22'),
('Tom Hanks', '1956-07-09'),
('Scarlett Johansson', '1984-11-22'),
('Denzel Washington', '1954-12-28'),
('Emma Stone', '1988-11-06'),
('Robert Downey Jr.', '1965-04-04'),
('Cate Blanchett', '1969-05-14');

-- Inserts para a tabela Diretor
INSERT INTO Diretor (Nome, DataNasc, Biografia) VALUES
('Quentin Tarantino', '1963-03-27', 'Diretor conhecido por seus filmes não lineares e diálogos marcantes.'),
('Christopher Nolan', '1970-07-30', 'Aclamado diretor por seus filmes complexos e narrativas inovadoras.'),
('Greta Gerwig', '1983-08-04', 'Diretora e roteirista com filmes focados em personagens femininas fortes.'),
('Steven Spielberg', '1946-12-18', 'Um dos diretores mais bem-sucedidos e influentes da história do cinema.'),
('Bong Joon-ho', '1969-09-13', 'Diretor sul-coreano conhecido por sua mistura de gêneros e comentários sociais.'),
('Denis Villeneuve', '1967-10-03', 'Diretor canadense conhecido por seus filmes de ficção científica e suspense visualmente impressionantes.'),
('Sofia Coppola', '1971-05-14', 'Diretora e roteirista conhecida por seu estilo visual e temas de isolamento.'),
('Taika Waititi', '1975-08-16', 'Diretor neozelandês conhecido por sua comédia peculiar e sensibilidade.'),
('Chloé Zhao', '1982-03-31', 'Diretora chinesa conhecida por seus filmes naturalistas e comoventes.'),
('Martin Scorsese', '1942-11-17', 'Um dos diretores mais importantes e influentes do cinema americano.');

-- Inserts para a tabela Filme
INSERT INTO Filme (Titulo, AnoLancamento, Sinopse, NotaIMDB, DiretorId) VALUES
('Pulp Fiction', 1994, 'As vidas de dois assassinos da máfia, um boxeador, a esposa de um gângster e um par de ladrões de restaurantes se entrelaçam em quatro histórias de violência e redenção.', 8.9, 1),
('Inception', 2010, 'Um ladrão que rouba segredos corporativos através da tecnologia de compartilhamento de sonhos recebe a tarefa impossível de plantar uma ideia na mente de um CEO.', 8.8, 2),
('Lady Bird', 2017, 'Uma jovem artista do ensino médio de Sacramento passa por seus últimos anos no ensino médio.', 7.4, 3),
('E.T. the Extra-Terrestrial', 1982, 'Um menino solitário faz amizade com um extraterrestre que ficou preso na Terra e tenta ajudá-lo a voltar para casa enquanto evita o governo.', 7.9, 4),
('Parasite', 2019, 'Toda a família desempregada de Ki-taek se interessa pelos ricos Park, aos poucos infiltrando-se em suas vidas e iniciando uma parasitária simbiose.', 8.5, 5),
('Dune', 2021, 'O herdeiro de uma família nobre é encarregado de proteger o recurso mais valioso e o planeta mais vital da galáxia.', 8.0, 6),
('Lost in Translation', 2003, 'Uma estrela de cinema americana e a jovem esposa de um fotógrafo se unem em Tóquio.', 7.7, 7),
('Thor: Ragnarok', 2017, 'Preso do outro lado do universo, o poderoso Thor se encontra em uma luta mortal contra o tempo para voltar a Asgard e impedir Ragnarok – a destruição de seu mundo e o fim da civilização asgardiana – nas mãos de uma nova e implacável ameaça, a implacável Hela.', 7.9, 8),
('Nomadland', 2020, 'Após perder tudo na crise financeira, uma mulher embarca em uma jornada pelo oeste americano vivendo em sua van como uma nômade moderna.', 7.3, 9),
('Goodfellas', 1990, 'A ascensão e queda de Henry Hill e seus associados da máfia ao longo de trinta anos.', 8.7, 10);

-- Inserts para a tabela Genero
INSERT INTO Genero (Nome) VALUES
('Drama'),
('Ação'),
('Comédia'),
('Ficção Científica'),
('Romance'),
('Suspense'),
('Terror'),
('Animação'),
('Aventura'),
('Fantasia');

-- Inserts para a tabela FilmeAtor
INSERT INTO FilmeAtor (FilmeId, AtorId, Papel) VALUES
(1, 1, 'Butch Coolidge'),
(1, 3, 'Jules Winnfield'),
(2, 3, 'Dom Cobb'),
(2, 6, 'Mal Cobb'),
(3, 8, 'Christine "Lady Bird" McPherson'),
(4, 4, 'Mary (voz)'),
(4, 5, 'Elliott Taylor'),
(5, 7, 'Ki-taek'),
(5, 10, 'Yeon-kyo'),
(6, 3, 'Paul Atreides'),
(6, 10, 'Lady Jessica'),
(7, 5, 'Bob Harris'),
(7, 6, 'Charlotte'),
(8, 9, 'Thor'),
(8, 10, 'Hela'),
(9, 4, 'Fern'),
(10, 5, 'Henry Hill'),
(10, 7, 'Tommy DeVito');

-- Inserts para a tabela FilmeGenero
INSERT INTO FilmeGenero (FilmeId, GeneroId) VALUES
(1, 1),
(1, 2),
(2, 2),
(2, 4),
(3, 1),
(3, 3),
(4, 4),
(4, 9),
(5, 1),
(5, 6),
(6, 4),
(6, 9),
(7, 1),
(7, 5),
(8, 2),
(8, 3),
(8, 10),
(9, 1),
(10, 1),
(10, 2);