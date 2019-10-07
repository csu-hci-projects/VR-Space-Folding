proceedings.pdf: proceedings.tex
	/usr/local/texlive/2014/bin/x86_64-darwin/pdflatex proceedings
	/usr/local/texlive/2014/bin/x86_64-darwin/bibtex proceedings
	/usr/local/texlive/2014/bin/x86_64-darwin/pdflatex proceedings
	/usr/local/texlive/2014/bin/x86_64-darwin/pdflatex proceedings
	open proceedings.pdf

clean:
	rm -f *.pdf
	rm -f *.log
	rm -f *.aux
	rm -f *.bbl
	rm -f *.blg
	
	